
using Aplication.IRepositories;
using Dapper;
using Domain;

namespace Persistence.Repositories
{
    public class ReservaMesaRepository : IReservaMesaRepository
    {
        private readonly DapperContext _context;
        public ReservaMesaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReservaMesa>> ListaReservaMesa(int reservaId)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"SELECT [ReservaMesaId]
      ,[ReservaId]
      ,[MesaId]
      ,[Fecha]
      ,[Hora]
	  , ZonaId
	    , Personas
  FROM [ReservaMesa] (nolock) where  ReservaId=@ReservaId order by ReservaMesaId desc";
            return await db.QueryAsync<ReservaMesa>(sql, new { reservaId = reservaId });
        }

        public async Task<IEnumerable<ReservaMesa>> ListaReservaMesaDia(DateTime fecha)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"SELECT resmesa.[ReservaMesaId]
      , resmesa.[ReservaId]
      , resmesa.[MesaId]
      , resmesa.[Fecha]
      , resmesa.[Hora]
	  , resmesa.ZonaId
	    , resmesa.Personas
  FROM [ReservaMesa] (nolock) resmesa
  left join Reservas (nolock) res on res.ReservaId=resmesa.ReservaId
  where   convert(date, resmesa.Fecha)=convert(date,@fecha)  
  order by  resmesa.ReservaMesaId desc";
            return await db.QueryAsync<ReservaMesa>(sql, new { fecha = fecha });
        }

        public async Task<IEnumerable<ReservaMesaCompleto>> ListaReservaMesaRango(DateTime fechaini,DateTime fechafin)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"SELECT 
res.[ReservaId]
      ,res.[Personas]
      ,res.[Fecha]
      ,res.[Hora]
      ,res.[ZonaId]
      ,res.[Nrodocumento]
      ,res.[Nombre]
      ,res.[Telefono]
      ,res.[Mensaje]
      ,res.Mascotas
,res.Motivo
  ,res.Correo
,res.Estado
,resmesa.[ReservaId]
      , resmesa.[MesaId]
	    , resmesa.Personas PersonasMesa
  FROM [ReservaMesa] (nolock) resmesa
  left join Reservas (nolock) res on res.ReservaId=resmesa.ReservaId
  where   convert(date, resmesa.Fecha) between convert(date,@fechaini)   and convert(date,@fechafin)  
  order by  res.[Fecha] desc ,res.[Hora] desc";
            return await db.QueryAsync<ReservaMesaCompleto>(sql, new { fechaini = fechaini, fechafin = fechafin });
        }
        public async Task<bool> CreateReservaMesa(ReservaMesa reserva)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"Insert into ReservaMesa ([ReservaId],[MesaId],[Fecha],[Hora],[Personas],[ZonaId]) 
                                         VALUES (@ReservaId,@MesaId,@Fecha,@Hora,@Personas,@ZonaId)";
            var result = await db.ExecuteAsync(
                    sql, reserva);
            return result > 0;
        }
    }
}
