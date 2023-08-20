using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        //private string conexionPrimary= "Data Source=DESKTOP-3D9MS7Q;Initial Catalog=BD_RESERVAS;User ID=sa;Password=147896321";
        //private string conexionSecondary = "Data Source=DESKTOP-3D9MS7Q;Initial Catalog=BD_RESERVAS_EXTERNO;User ID=sa;Password=147896321";
        private readonly string _connectionStringPrimary;
        private readonly string _connectionStringSecondary;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionStringPrimary =   _configuration.GetConnectionString("DefConnectionPrimary");//conexionPrimary;
            _connectionStringSecondary =  _configuration.GetConnectionString("DefConnectionSecondary");//conexionSecondary;

        }

        public IDbConnection CreateConnectionPrimary() => new SqlConnection(_connectionStringPrimary);
        public IDbConnection CreateConnectionSecondary() => new SqlConnection(_connectionStringSecondary);

    }
}