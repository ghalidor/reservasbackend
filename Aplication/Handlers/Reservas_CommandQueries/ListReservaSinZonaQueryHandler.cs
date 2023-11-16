

using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;
using System.Globalization;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class ListReservaSinZonaQueryHandler : IRequestHandler<ListReservaSinZonaQuery, IEnumerable<ReservacionLista>>
    {
        private readonly IReservasRepository _reservasRepository;
        private readonly IMesasRepository _mesasRepository;
        private readonly IZonasRepository _zonasRepository;
        private readonly IReservaMesaRepository _reservaMesaRepository;
        public ListReservaSinZonaQueryHandler(IReservasRepository reservasRepository,
            IZonasRepository zonasRepository, IReservaMesaRepository reservaMesaRepository,
            IMesasRepository mesasRepository)
        {
            _reservasRepository = reservasRepository;
            _zonasRepository = zonasRepository;
            _mesasRepository = mesasRepository;
            _reservaMesaRepository = reservaMesaRepository;
        }
        public async Task<IEnumerable<ReservacionLista>> Handle(ListReservaSinZonaQuery query, CancellationToken cancellationToken)
        {
            List<ReservacionLista> lista = new List<ReservacionLista>();
            try
            {
                ReservacionLista registro = new ReservacionLista();
                var reservas = await _reservasRepository.ListaReservacion(Convert.ToDateTime(query.fechaini), Convert.ToDateTime(query.fechafin));//mesas detalle de reserva
                DateTime now = DateTime.Now;
                if(reservas.Any())
                {
                    foreach(var reserva in reservas)
                    {

                        registro = new ReservacionLista();
                        DateTime fechaReserva = Convert.ToDateTime(reserva.Fecha.ToString("yyyy-MM-dd") + " " + reserva.Hora, CultureInfo.InvariantCulture);
                        if(fechaReserva < now)
                        {
                            registro.ClaseEstado = "pasado";
                        }
                        else
                        {
                            if(fechaReserva.Date == now.Date)
                            {
                                registro.ClaseEstado = "hoy";
                            }
                            else
                            {
                                registro.ClaseEstado = "futuro";
                            }
                        }
                        registro.Estado = reserva.Estado == 0 ? "NO VINO" : reserva.Estado == 1 ? "REGISTRADO" : reserva.Estado == 2 ? "CANCELADO" : reserva.Estado == 3 ? "EN ATENCION" : "ATENDIDO";
                        registro.FechaDatetime = fechaReserva;
                        registro.Fecha = fechaReserva.ToString("dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture);
                        registro.ReservaId = reserva.ReservaId;

                        registro.Hora = reserva.Hora;


                        registro.Personas = reserva.Personas;
                        registro.Mascotas = reserva.Mascotas;
                        registro.MascotasString = reserva.Mascotas ? "SI" : "NO";
                        registro.Mensaje = reserva.Mensaje;
                        registro.Telefono = reserva.Telefono;
                        registro.Nombre = reserva.Nombre;
                        registro.Correo = reserva.Correo;
                        registro.Motivo = reserva.Motivo;
                        lista.Add(registro);
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return lista.OrderByDescending(x => x.FechaDatetime);
        }
    }
}
