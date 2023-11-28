

using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class ListaHorasQueryHandler : IRequestHandler<ListaHorasQuery, ServiceResponseReserva>
    {
        private readonly IReservasRepository _reservasRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IMesasRepository _mesasRepository;
        private readonly IZonasRepository _zonasRepository;
        private readonly IReservaMesaRepository _reservaMesaRepository;
        private readonly IConfiguration _configuracion;
        public ListaHorasQueryHandler(IReservasRepository reservasRepository, IConfiguration configuracion,
            IEmpresaRepository empresaRepository, IMesasRepository mesasRepository, IZonasRepository zonasRepository,
            IReservaMesaRepository reservaMesaRepository)
        {
            _reservasRepository = reservasRepository;
            _empresaRepository = empresaRepository;
            _mesasRepository = mesasRepository;
            _zonasRepository = zonasRepository;
            _reservaMesaRepository = reservaMesaRepository;
            _configuracion = configuracion;
        }

        public async Task<ServiceResponseReserva> Handle(ListaHorasQuery request, CancellationToken cancellationToken)
        {
           
            ServiceResponseReserva response = new ServiceResponseReserva();
            try
            {
                int intervalo = Convert.ToInt32(_configuracion["variables:intervalo"]);
                var empresa = await _empresaRepository.RegistroEmpresa();
                string horaInicioString = $"{DateTime.Now.ToString("yyyy-MM-dd")} {empresa.AtencionHoraInicio}";
                DateTime horaInicioDate = Convert.ToDateTime(horaInicioString);
                int horaInicio = horaInicioDate.Hour;
                string horaFinString = $"{DateTime.Now.ToString("yyyy-MM-dd")} {empresa.AtencionHoraFin}";
                DateTime horaFinoDate = Convert.ToDateTime(horaFinString);
                int horaFin = horaFinoDate.Hour;

 
                List<ReservacionHoras> horasLista = new List<ReservacionHoras>();
                ReservacionHoras poruno = new ReservacionHoras();
                List<Zonas> zonasxHora = new List<Zonas>();
                for(int i = horaInicio; i <= horaFin; i++)
                {
                    poruno = new ReservacionHoras();
                    string horaString = (i < 10) ? $"0{i}" : i.ToString();
                    horaString = horaString + ":"+intervalo;
                    horaString = (i < 12) ? $"{horaString} AM" : $"{horaString} PM";

                    poruno.Fecha = Convert.ToDateTime(DateTime.Now);
                    poruno.Hora = horaString;
                    poruno.IsActivo = true;
                    poruno.ZonasLibres = zonasxHora.ToList();
                    horasLista.Add(poruno);


                }

                response.lista = horasLista;
                response.message = "Registros Libres";
                response.response = true;
            }
            catch(Exception ex)
            {
                response.message = ex.Message;

            }

            return response;

        }

    }
}
