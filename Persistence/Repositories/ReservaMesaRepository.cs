
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
