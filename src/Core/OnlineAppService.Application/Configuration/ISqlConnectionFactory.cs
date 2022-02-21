using System.Data;

namespace OnlineAppService.Application.Configuration
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
