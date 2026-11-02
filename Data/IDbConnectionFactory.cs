using System.Data;

namespace WildTrailsIndia.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
