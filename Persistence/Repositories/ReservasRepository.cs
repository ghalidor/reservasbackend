
using Aplication.IRepositories;
using Dapper;
using Domain;

namespace Persistence.Repositories
{
    public class ReservasRepository: IReservasRepository
    {
        private readonly DapperContext _context;
        public ReservasRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservas>> ListaReservacion(DateTime fechaini,DateTime fechafin)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"SELECT [ReservaId]
      ,[Personas]
      ,[Fecha]
      ,[Hora]
        ,MesaId
      ,[ZonaId]
      ,[Nrodocumento]
      ,[Nombre]
      ,[Telefono]
      ,[Mensaje]
      ,Mascotas
,Correo
    ,Estado
  FROM [Reservas] (nolock) where convert(date,Fecha) between convert(date,@fechaini) and convert(date,@fechafin) order by ReservaId desc";
            return await db.QueryAsync<Reservas>(sql, new {fechaini=fechaini,fechafin=fechafin});
        }

        public async Task<IEnumerable<Reservas>> ListaReservacionxDia(DateTime dia)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"SELECT [ReservaId]
      ,[Personas]
      ,[Fecha]
      ,[Hora]
        ,MesaId
      ,[ZonaId]
      ,[Nrodocumento]
      ,[Nombre]
      ,[Telefono]
      ,[Mensaje]
       ,Mascotas
,Correo
 ,Estado
  FROM [Reservas] (nolock) where  convert(date,Fecha)=convert(date,@dia) order by ReservaId desc";
            return await db.QueryAsync<Reservas>(sql, new {dia=dia});
        }

        public async Task<bool> CreateReserva(Reservas reserva)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"Insert into Reservas ([Personas],[Fecha],[Hora],MesaId,[ZonaId],[Nrodocumento],[Nombre],[Telefono],[Mensaje],Mascotas,Correo) 
                VALUES (@Personas,@Fecha,@Hora,@MesaId,@ZonaId,@Nrodocumento,@Nombre,@Telefono,@Mensaje,@Mascotas,@Correo)";
            var result = await db.ExecuteAsync(
                    sql, reserva);
            return result > 0;
        }

        public async Task<int> CreateReservaReturnId(Reservas reserva)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"Insert into Reservas ([Personas],[Fecha],[Hora],MesaId,[ZonaId],[Nrodocumento],[Nombre],[Telefono],[Mensaje],Mascotas,Estado,Correo) 
                VALUES (@Personas,@Fecha,@Hora,@MesaId,@ZonaId,@Nrodocumento,@Nombre,@Telefono,@Mensaje,@Mascotas,1,@Correo)
                       SELECT SCOPE_IDENTITY()";
            var result = await db.QueryAsync<int>(sql, reserva);
            return result.Single();
        }

        public async Task<bool> UpdateReserva(Reservas reserva)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"UPDATE Reservas
                                SET
                                    Personas = @Personas,
                                    Fecha = @Fecha,
                                    Hora = @Hora,
                                    MesaId = @MesaId,
                                    ZonaId = @ZonaId,
                                    Nrodocumento = @Nrodocumento,
                                    Nombre = @Nombre,
                                    Telefono = @Telefono,
                                    Mascotas=@Mascotas,
 Correo=@Correo,
                                    Mensaje = @Mensaje where ReservaId=@ReservaId";
            var result = await db.ExecuteAsync(
                    sql, reserva);
            return result > 0;
        }

        public async Task<bool> UpdateReservaEstado(Reservas reserva)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"UPDATE Reservas
                                SET
                                    Estado = @Estado ,Motivo=@Motivo where ReservaId=@ReservaId";
            var result = await db.ExecuteAsync(
                    sql, reserva);
            return result > 0;
        }

        public async Task<bool> DeleteReserva(int reservaId)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = "Delete from Reservas where ReservaId=@ReservaId";
            var result = await db.ExecuteAsync(
                    sql, new { ReservaId = reservaId });
            return result > 0;
        }

    }
}
