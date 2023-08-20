
using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;
using System.Globalization;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class ListHorasLibreReservasQueryHandler : IRequestHandler<ListHorasLibreReservasQuery, ServiceResponseReserva>
    {
        private readonly IReservasRepository _reservasRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IMesasRepository _mesasRepository;
        private readonly IZonasRepository _zonasRepository;
        private readonly IReservaMesaRepository _reservaMesaRepository;
        public ListHorasLibreReservasQueryHandler(IReservasRepository reservasRepository,
            IEmpresaRepository empresaRepository, IMesasRepository mesasRepository, IZonasRepository zonasRepository,
            IReservaMesaRepository reservaMesaRepository)
        {
            _reservasRepository = reservasRepository;
            _empresaRepository = empresaRepository;
            _mesasRepository = mesasRepository;
            _zonasRepository = zonasRepository;
            _reservaMesaRepository = reservaMesaRepository;
        }

        public async Task<ServiceResponseReserva> Handle(ListHorasLibreReservasQuery request, CancellationToken cancellationToken)
        {
            if(request.fecha is null)
            {
                throw new ApplicationException("There is a problem in mapper");
            }
            ServiceResponseReserva response = new ServiceResponseReserva();
            try
            {

                var empresa = await _empresaRepository.RegistroEmpresa();
                string horaInicioString = $"{DateTime.Now.ToString("yyyy-MM-dd")} {empresa.AtencionHoraInicio}";
                DateTime horaInicioDate = Convert.ToDateTime(horaInicioString);
                int horaInicio = horaInicioDate.Hour;
                string horaFinString = $"{DateTime.Now.ToString("yyyy-MM-dd")} {empresa.AtencionHoraFin}";
                DateTime horaFinoDate = Convert.ToDateTime(horaFinString);
                int horaFin = horaFinoDate.Hour;

                var zonasRegistro = await _zonasRepository.ListZonas();
                //var reservasdelDia = await _reservasRepository.ListaReservacionxDia(Convert.ToDateTime(request.fecha));
                var mesasParaReservar = await _mesasRepository.ListMesasLibres();
                var mesasOcupadas = await _reservaMesaRepository.ListaReservaMesaDia(Convert.ToDateTime(request.fecha));//mesas detalle de reserva

                List<ReservacionHoras> horasLista = new List<ReservacionHoras>();
                ReservacionHoras poruno =  new ReservacionHoras();
                List<Zonas> zonasxHora = new List<Zonas>();
                for(int i = horaInicio; i <= horaFin; i++)
                {
                    string horaString = (i < 10) ? $"0{i}" : i.ToString();
                    horaString = horaString + ":00";
                    horaString = (i < 12) ? $"{horaString} AM" : $"{horaString} PM";
                    string horaNocIni = $"{request.fecha} {horaString}";
                    DateTime turnohoraIni = Convert.ToDateTime(horaNocIni, CultureInfo.InvariantCulture);
                    DateTime horaAntes = turnohoraIni.AddHours(-1);
                    DateTime horaDespues = turnohoraIni.AddHours(1);
                    string antes = horaAntes.ToString("HH:mm tt", CultureInfo.InvariantCulture);
                    string despues = horaDespues.ToString("HH:mm tt", CultureInfo.InvariantCulture);
                    zonasxHora = new List<Zonas>();
                    poruno = new ReservacionHoras();
                    Zonas regPrueba = new Zonas();

                    List<ReservaMesa> listaexisteReservaHora = mesasOcupadas.Where(x => x.Hora.Trim() == horaString).ToList();
                    //int[] mesasIdOcupadasHora = listaexisteReservaHora.Select(z => z.MesaId).ToArray();
                    foreach(var registro in zonasRegistro)
                    {
                        regPrueba = new Zonas();
                        regPrueba.ZonaId = registro.ZonaId;
                        regPrueba.Descripcion=registro.Descripcion;
                        regPrueba.EmpresaId=registro.EmpresaId;
                        regPrueba.SucursalId=registro.SucursalId;
                        regPrueba.Servidor=registro.Servidor;

                        int[] mesasOcupadasID = new int[] { };
                        if(listaexisteReservaHora.Count > 0)
                        {
                            mesasOcupadasID = listaexisteReservaHora.Where(x =>x.ZonaId==registro.ZonaId).Select(a => a.MesaId).ToArray();
                        }

                        List<ReservaMesa> reservaAnterior = mesasOcupadas.Where(x => x.Hora.Trim() == antes
                        && x.ZonaId==registro.ZonaId ).ToList();
                        int[] IDreservaAnterior = reservaAnterior.Select(z => z.ReservaId).ToArray();
                        int[] mesasOcupadasAnterior = mesasOcupadas.Where(z => IDreservaAnterior.Contains(z.ReservaId)).Select(z => z.MesaId).ToArray();


                        List<ReservaMesa> reservaDespues = mesasOcupadas.Where(x => x.Hora.Trim() == despues
                         && x.ZonaId == registro.ZonaId).ToList();
                        int[] IDreservaDespues = reservaDespues.Select(z => z.ReservaId).ToArray();
                        var mesasOcupadasDespues = mesasOcupadas.Where(z => IDreservaDespues.Contains(z.ReservaId)).Select(z => z.MesaId).ToArray();


                        //mesas para reservar en la zona total de la hora i
                        var mesesLibreEnlaZona = mesasParaReservar.Where(x => x.ZonaId == registro.ZonaId && x.ParaReservar == true &&
                        !mesasOcupadasID.Contains(x.MesaId));

                        bool existe = mesesLibreEnlaZona.Where(x => !mesasOcupadasAnterior.Contains(x.MesaId) && !mesasOcupadasDespues.Contains(x.MesaId)).Any();

                        if(!existe)
                        {
                            regPrueba.EsActivo = false;
                        }
                        else{
                            regPrueba.EsActivo = true;
                        }
                        zonasxHora.Add(regPrueba);   
                    }

                    bool horaActivo = zonasxHora.Where(z => z.EsActivo).Count() > 0;
                    poruno.Fecha = Convert.ToDateTime(request.fecha);
                    poruno.Hora = horaString;
                    poruno.IsActivo = horaActivo;
                    poruno.ZonasLibres = zonasxHora.ToList();
                    horasLista.Add(poruno);
                }

                response.lista=horasLista;
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
