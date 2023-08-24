
using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;
using System.Globalization;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class ListReservasQueryHandler : IRequestHandler<ListReservasQuery, IEnumerable<ReservacionLista>>
    {
        private readonly IReservasRepository _reservasRepository;
        private readonly IMesasRepository _mesasRepository;
        private readonly IZonasRepository _zonasRepository;
        private readonly IReservaMesaRepository _reservaMesaRepository;
        public ListReservasQueryHandler(IReservasRepository reservasRepository,
            IZonasRepository zonasRepository, IReservaMesaRepository reservaMesaRepository,
            IMesasRepository mesasRepository)
        {
            _reservasRepository = reservasRepository; 
            _zonasRepository = zonasRepository;
            _mesasRepository = mesasRepository;
            _reservaMesaRepository = reservaMesaRepository;
        }
        public async Task<IEnumerable<ReservacionLista>> Handle(ListReservasQuery query, CancellationToken cancellationToken)
        {
            List<ReservacionLista> lista = new List<ReservacionLista>();
            try
            {
                var mesas =await _mesasRepository.ListMesas();
                var zonas = await _zonasRepository.ListZonas();
                ReservacionLista  registro = new ReservacionLista();
                DateTime now = DateTime.Now;
                if(mesas.Any() && zonas.Any())
                {
                    var reservasMesas = await _reservaMesaRepository.ListaReservaMesaRango(Convert.ToDateTime(query.fechaini), Convert.ToDateTime(query.fechafin));//mesas detalle de reserva
                    var reservacaiones = reservasMesas.GroupBy(n => new { n.ReservaId, n.Personas, n.Fecha,n.Hora,n.ZonaId,n.NroDocumento,n.Nombre,n.Telefono,n.Mensaje,n.Mascotas,n.Estado,n.Correo,n.Motivo }).ToList();

                    if(reservacaiones.Any())
                    {
                        foreach(var reserva in reservacaiones)
                        {
                            var cantidadMesas = reservasMesas.Where(z => z.ReservaId==reserva.Key.ReservaId).Select(x=>x.MesaId).ToArray();
                            registro = new ReservacionLista();
                            DateTime fechaReserva = Convert.ToDateTime(reserva.Key.Fecha.ToString("yyyy-MM-dd")+" "+ reserva.Key.Hora, CultureInfo.InvariantCulture);
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
                            registro.Estado = reserva.Key.Estado == 0 ? "NO VINO" : reserva.Key.Estado == 1 ? "REGISTRADO" : reserva.Key.Estado == 2 ? "CANCELADO" :reserva.Key.Estado==3?"EN ATENCION": "ATENDIDO";
                            registro.FechaDatetime = fechaReserva;
                            registro.Fecha = fechaReserva.ToString("dd-MM-yyyy hh:mm tt",CultureInfo.InvariantCulture);
                            registro.ReservaId = reserva.Key.ReservaId;
                            //registro.MesaId = reserva.Key.MesaId;
                            registro.Mesa = string.Join(",", mesas.Where(z => cantidadMesas.Contains(z.MesaId)).Select(x => x.Descripcion).ToArray());
                            registro.NroMesa = string.Join(",",mesas.Where(z => cantidadMesas.Contains(z.MesaId)).Select(x => x.NumeroMesa.ToString()).ToArray());
                            registro.Hora= reserva.Key.Hora;
                            registro.Zona= zonas.Where(z=>z.ZonaId== reserva.Key.ZonaId).Select(z => z.Descripcion).FirstOrDefault();
                            
                            registro.Personas = reserva.Key.Personas;
                            registro.Mascotas = reserva.Key.Mascotas;
                            registro.MascotasString = reserva.Key.Mascotas?"SI":"NO";
                            registro.Mensaje = reserva.Key.Mensaje;
                            registro.Telefono = reserva.Key.Telefono;
                            registro.Nombre = reserva.Key.Nombre;
                            registro.Correo = reserva.Key.Correo;
                            registro.Motivo= reserva.Key.Motivo;
                            lista.Add(registro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return lista.OrderByDescending(x=>x.FechaDatetime);
        }
    }
}
