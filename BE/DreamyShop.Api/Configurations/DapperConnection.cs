using System.Data;
using System.Data.SqlClient;

namespace DreamyShop.Api.Configurations
{
    public class DapperConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DreamyShopDBContext");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
