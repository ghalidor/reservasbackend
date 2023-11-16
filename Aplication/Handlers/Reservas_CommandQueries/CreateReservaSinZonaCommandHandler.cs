

using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Utilitarios;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class CreateReservaSinZonaCommandHandler : IRequestHandler<CreateReservaSinZonaCommand, ServiceResponse>
    {
        private readonly IReservasRepository _reservasRepository;
        private readonly IReservaMesaRepository _reservaMesaRepository;
        private readonly IMesasRepository _mesasRepository;
        private readonly IConfiguration _configuracion;
        public CreateReservaSinZonaCommandHandler(IReservasRepository reservasRepository, IMesasRepository mesasRepository, IReservaMesaRepository reservaMesaRepository, IConfiguration configuracion)
        {
            _reservasRepository = reservasRepository;
            _mesasRepository = mesasRepository;
            _reservaMesaRepository = reservaMesaRepository;
            _configuracion = configuracion;
        }

        public async Task<ServiceResponse> Handle(CreateReservaSinZonaCommand request, CancellationToken cancellationToken)
        {
            if(request.NewReservas is null)
            {
                throw new ApplicationException("There is a problem in mapper");
            }
            ServiceResponse response = new ServiceResponse();
            ReservacionNuevoSinZona reserva = request.NewReservas;

            try
            {
                int intervalo = Convert.ToInt32(_configuracion["variables:intervalo"]);
                CorreoSends correo = new CorreoSends(_configuracion);
                Reservas reservaNuevo = new Reservas();
                reservaNuevo.Personas = reserva.Personas;
                reservaNuevo.Fecha = reserva.Fecha;
                reservaNuevo.Hora = reserva.Hora;
                reservaNuevo.ZonaId =0;
                reservaNuevo.NroDocumento = reserva.NroDocumento;
                reservaNuevo.Nombre = reserva.Nombre;
                reservaNuevo.Telefono = reserva.Telefono;
                reservaNuevo.Mensaje = reserva.Mensaje;
                reservaNuevo.Mascotas = reserva.Mascotas;
                reservaNuevo.Correo = reserva.Correo;

                string horaReservaActual = $"{reserva.Fecha.ToString("yyyy-MM-dd")} {reserva.Hora}";
                DateTime horaDateReservaActual = Convert.ToDateTime(horaReservaActual);
                DateTime horaAnteriorDate = horaDateReservaActual.AddHours(Convert.ToInt32($"-{intervalo}"));
                DateTime horaDespuesDate = horaDateReservaActual.AddHours(intervalo);

                int idreserva = await _reservasRepository.CreateReservaReturnId(reservaNuevo);
                if(idreserva > 0)
                {
                    
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
            catch(Exception ex)
            {
                response.message = "Error al Registrar, " + ex.Message;
            }
            return response;
        }
    }
}
