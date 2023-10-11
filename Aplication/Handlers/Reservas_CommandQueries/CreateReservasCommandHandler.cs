
using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Utilitarios;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class CreateReservasCommandHandler : IRequestHandler<CreateReservasCommand, ServiceResponse>
    {
        private readonly IReservasRepository _reservasRepository;
        private readonly IReservaMesaRepository _reservaMesaRepository;
        private readonly IMesasRepository _mesasRepository;
        private readonly IZonasRepository _zonasRepository;
        private readonly IConfiguration _configuracion;
        public CreateReservasCommandHandler(IReservasRepository reservasRepository, IMesasRepository mesasRepository,
            IZonasRepository zonasRepository, IReservaMesaRepository reservaMesaRepository, IConfiguration configuracion)
        {
            _reservasRepository = reservasRepository;
            _mesasRepository = mesasRepository;
            _zonasRepository = zonasRepository;
            _reservaMesaRepository = reservaMesaRepository;
            _configuracion = configuracion;
        }

        public async Task<ServiceResponse> Handle(CreateReservasCommand request, CancellationToken cancellationToken)
        {
            if(request.NewReservas is null)
            {
                throw new ApplicationException("There is a problem in mapper");
            }
            ServiceResponse response = new ServiceResponse();
            ReservacionNuevo reserva = request.NewReservas;

            try
            { 
                int intervalo = Convert.ToInt32(_configuracion["variables:intervalo"]);
                CorreoSends correo = new CorreoSends(_configuracion);
                Reservas reservaNuevo = new Reservas();
                reservaNuevo.Personas = reserva.Personas;
                reservaNuevo.Fecha = reserva.Fecha;
                reservaNuevo.Hora = reserva.Hora;
                reservaNuevo.ZonaId = reserva.ZonaId;
                reservaNuevo.NroDocumento = reserva.NroDocumento;
                reservaNuevo.Nombre = reserva.Nombre;
                reservaNuevo.Telefono = reserva.Telefono;
                reservaNuevo.Mensaje = reserva.Mensaje;
                reservaNuevo.Mascotas = reserva.Mascotas;
                reservaNuevo.Correo = reserva.Correo;
                var mesasParaReservar = await _mesasRepository.ListMesasLibres();
                var reservasdeDia = await _reservasRepository.ListaReservacionxDia(reserva.Fecha);
                var mesas = await _reservaMesaRepository.ListaReservaMesaDia(reserva.Fecha);//mesas detalle de reserva

                var reservas = reservasdeDia.Where(x => x.Hora == reserva.Hora && x.ZonaId == reserva.ZonaId).ToList();
                if(reservas.Count() > 0)
                {
                    var cantidadenZonaOcupadas = mesas.Where(z => reservas.Select(w => w.ReservaId).ToArray().Contains(z.ReservaId)).ToList();
                    if(cantidadenZonaOcupadas != null)
                    {
                        int[] mesasID = cantidadenZonaOcupadas.Select(x => x.MesaId).ToArray();
                        mesasParaReservar = mesasParaReservar.Where(z => !mesasID.Contains(z.MesaId) && z.ZonaId == reserva.ZonaId);
                    }
                }
                else
                {
                    mesasParaReservar = mesasParaReservar.Where(z => z.ZonaId == reserva.ZonaId);
                }

                var mesaenZona = mesasParaReservar.ToList();
                if(!mesaenZona.Any())
                {
                    response.response = false;
                    response.message = $"No hay Mesas Disponible en la Zona para las {reservaNuevo.Hora}.";
                }
                else
                {
                    string horaReservaActual = $"{reserva.Fecha.ToString("yyyy-MM-dd")} {reserva.Hora}";
                    DateTime horaDateReservaActual = Convert.ToDateTime(horaReservaActual);
                    DateTime horaAnteriorDate = horaDateReservaActual.AddHours(Convert.ToInt32($"-{intervalo}"));
                    DateTime horaDespuesDate = horaDateReservaActual.AddHours(intervalo);

                    List<int> listamesasCantPersonas = mesaenZona.Select(x => x.Pax).ToList();
                    seleccionMesa seleccionar = new seleccionMesa();
                    var mesasElegida = seleccionar.mesaEscoger(listamesasCantPersonas, reserva.Personas);

                    if(mesasElegida.Numbers.Any())
                    {
                        int[] indexEscogidas = mesasElegida.Numbers.Select(x=>x.Index).ToArray();
                        mesaenZona = mesaenZona.Where((mesa__, index) => indexEscogidas.Contains(index)).ToList();

                        //reservaNuevo.MesaId = mesaenZona[0].MesaId;
                        int idreserva = await _reservasRepository.CreateReservaReturnId(reservaNuevo);
                        if(idreserva > 0)
                        {
                            ReservaMesa mesaReserva = new ReservaMesa();
                            foreach(var registro in mesaenZona)
                            {
                                mesaReserva = new ReservaMesa();
                                mesaReserva.ReservaId = idreserva;
                                mesaReserva.MesaId = registro.MesaId;
                                mesaReserva.ZonaId = registro.ZonaId;
                                mesaReserva.Hora = reserva.Hora;
                                mesaReserva.Fecha = reserva.Fecha;
                                mesaReserva.Personas = registro.Pax;

                                await _reservaMesaRepository.CreateReservaMesa(mesaReserva);
                            }
                            response.response = true;
                            response.message = "Registrado Corréctamente";

                            string destinatarios = _configuracion["variables:destinatarios"];
                            List<string> destinatarios_lista = destinatarios.Split(',').ToList();
                            destinatarios_lista.Add(reserva.Correo);
                            string listaCorreosEnviar = String.Join(",", destinatarios_lista);
                            correo.envio_correoRemoto(reservaNuevo, listaCorreosEnviar);
                        }
                        else
                        {
                            response.response = false;
                            response.message = "Error al intentar Registrar";
                        }
                    }
                    else
                    {
                        response.response = false;
                        response.message = "No hay mesas Disponibles";

                    }
                    
                }
            }
            catch(Exception ex)
            {
                response.message = "Error al Registrar, " + ex.Message;
            }
            return response;
        }
    }
}
