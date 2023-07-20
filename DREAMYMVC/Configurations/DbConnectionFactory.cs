using System.Data.SqlClient;
using System.Data;

namespace DREAMYMVC.Configurations
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DreamyShopDBContext");
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
            }
            else if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }
    }

}
