
using Aplication.IRepositories;
using Dapper;
using Domain;

namespace Persistence.Repositories
{
    public class EmpresaRepository: IEmpresaRepository
    {
        private readonly DapperContext _context;
        public EmpresaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Empresa> RegistroEmpresa()
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"SELECT [Empresa_id]
      ,[Nombre]
      ,[AtencionDiaInicio]
      ,[AtencionDiaFin]
      ,[AtencionHoraInicio]
      ,[AtencionHoraFin]
      ,[Telefono]
,[direccion]
,[Personas]
  FROM Empresa (nolock) order by Empresa_id desc";
            return await db.QueryFirstOrDefaultAsync<Empresa>(sql);
        }

        public async Task<bool> UpdateEmpresa(Empresa empresa)
        {
            var db = _context.CreateConnectionSecondary();
            var sql = @"UPDATE Empresa
                                SET
                                    Nombre = @Nombre,
                                    AtencionDiaInicio = @AtencionDiaInicio,
                                    AtencionDiaFin = @AtencionDiaFin,
                                    AtencionHoraInicio = @AtencionHoraInicio,
                                    AtencionHoraFin = @AtencionHoraFin,
                                    Telefono = @Telefono,
                                    Personas=@Personas,
direccion=@direccion
                                    where Empresa_id=@Empresa_id";
            var result = await db.ExecuteAsync(
                    sql, empresa);
            return result > 0;
        }
    }
}
