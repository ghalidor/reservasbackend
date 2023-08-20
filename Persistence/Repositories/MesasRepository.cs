using Aplication.IRepositories;
using Dapper;
using Domain;

namespace Persistence.Repositories
{
    public class MesasRepository : IMesasRepository
    {
        private readonly DapperContext _context;
        public MesasRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mesas>> ListMesas()
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"SELECT [MesaId]
      ,[Descripcion]
      ,[NumeroMesa]
      ,[EsActiva]
      ,[Ocupada]
      ,[SucursalId]
      ,[ZonaId]
      ,[EmpresaId]
      ,[Servidor]
      ,[Tipo]
      ,[Pax]
      ,[ParaReservar]
  FROM [Mesas] order by MesaId desc";
            return await db.QueryAsync<Mesas>(sql);
        }

        public async Task<IEnumerable<Mesas>> ListMesasLibres()
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"SELECT [MesaId]
      ,[Descripcion]
      ,[NumeroMesa]
      ,[EsActiva]
      ,[Ocupada]
      ,[SucursalId]
      ,[ZonaId]
      ,[EmpresaId]
      ,[Servidor]
      ,[Tipo]
      ,[Pax]
      ,[ParaReservar]
  FROM [Mesas] where ParaReservar=1  order by MesaId desc";
            return await db.QueryAsync<Mesas>(sql);
        }

        public async Task<bool> CreateMesas(Mesas mesas)
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"Insert into Mesas ([Descripcion],[NumeroMesa],[EsActiva],[Ocupada],[SucursalId],[ZonaId],[EmpresaId],[Servidor],[Tipo],[Pax],[ParaReservar]) 
                VALUES (@Descripcion,@NumeroMesa,@EsActiva,@Ocupada,@SucursalId,@ZonaId,@EmpresaId,@Servidor,@Tipo,@Pax,@ParaReservar)";
            var result = await db.ExecuteAsync(
                    sql, mesas);
            return result > 0;
        }

        public async Task<bool> UpdateMesas(Mesas mesas)
        {
            var db = _context.CreateConnectionPrimary();
            var sql = @"UPDATE Mesas
                                SET
                                   [Descripcion] = @Descripcion
      ,[NumeroMesa] = @NumeroMesa
      ,[EsActiva] = @EsActiva
      ,[Ocupada] = @Ocupada
      ,[SucursalId] = @SucursalId
      ,[ZonaId] = @ZonaId
      ,[EmpresaId] = @EmpresaId
      ,[Servidor] =@Servidor
      ,[Tipo] = @Tipo
      ,[Pax] = @Pax
      ,[ParaReservar] = @ParaReservar where MesaId=@MesaId";
            var result = await db.ExecuteAsync(
                    sql, mesas);
            return result > 0;
        }

        public async Task<bool> DeleteMesa(int mesaId)
        {
            var db = _context.CreateConnectionPrimary();
            var sql = "Delete from Mesas where MesaId=@mesaId";
            var result = await db.ExecuteAsync(
                    sql, new { mesaId = mesaId });
            return result > 0;
        }
    }
}
