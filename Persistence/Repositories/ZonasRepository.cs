
using Aplication.IRepositories;
using Dapper;
using Domain;

namespace Persistence.Repositories
{
    public class ZonasRepository: IZonasRepository
    {
        private readonly DapperContext _context;
        public ZonasRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Zonas>> ListZonas()
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"SELECT [ZonaId]
      ,[Descripcion]
      ,[SucursalId]
      ,[EsActivo]
      ,[EmpresaId]
      ,[Servidor]
  FROM [Zona] order by ZonaId desc";
            return await db.QueryAsync<Zonas>(sql);
        }

        public async Task<Zonas> ZonaDetalle(int zonaid)
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"SELECT [ZonaId]
      ,[Descripcion]
      ,[SucursalId]
      ,[EsActivo]
      ,[EmpresaId]
      ,[Servidor]
  FROM [Zona] where ZonaId=@zonaid order by ZonaId desc";
            return await db.QueryFirstOrDefaultAsync<Zonas>(sql, new { zonaid = zonaid });
        }


        public async Task<IEnumerable<ZonasMesasAsignadas>> ListMesasAsignadasZona()
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"  select zona.ZonaId, zona.Descripcion,  count(mesa.ZonaId) Asignadas ,count(CASE
                  WHEN mesa.ParaReservar = 1
                     THEN 1
             END) paraReserva
	 from [Mesas] mesa 
	 left join Zona zona on zona.ZonaId=mesa.ZonaId
	 where mesa.ZonaId!=0
	 group by zona.ZonaId,zona.Descripcion,mesa.ZonaId";
            return await db.QueryAsync<ZonasMesasAsignadas>(sql);
        }

        public async Task<bool> CreateZona(Zonas zona)
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"Insert into Zona ([Descripcion],[SucursalId],[EsActivo],[EmpresaId],[Servidor]) 
                                   VALUES (@Descripcion,@SucursalId,@EsActivo,@EmpresaId,@Servidor)";
            var result = await db.ExecuteAsync(
                    sql, zona);
            return result > 0;
        }

        public async Task<bool> UpdateZona(Zonas zona)
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"UPDATE Zona
                                SET
                                   [Descripcion] = @Descripcion
      ,[SucursalId] = @SucursalId
      ,[EsActivo] = @EsActivo
      ,[EmpresaId] = @EmpresaId
      ,[Servidor] = @Servidor
    where ZonaId=@ZonaId";
            var result = await db.ExecuteAsync(
                    sql, zona);
            return result > 0;
        }

        public async Task<bool> DeleteZona(int ZonaId)
        {
            var db = _context.CreateConnectionPrimary();
            var sql = "Delete from Zona where ZonaId=@ZonaId";
            var result = await db.ExecuteAsync(
                    sql, new { ZonaId = ZonaId });
            return result > 0;
        }


    }
}
