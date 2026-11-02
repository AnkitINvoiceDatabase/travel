using Microsoft.Data.SqlClient;
using System.Data;

namespace WildTrailsIndia.Data
{
    public class DapperConnectionFactory: IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DapperConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString =
                _configuration.GetConnectionString("TravelXConnection");

            return new SqlConnection(connectionString);
        }
    }
}
