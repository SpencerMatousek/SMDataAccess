using SMDataAccess.Data.IData;
using SMDataAccess.Data;
using Microsoft.Extensions.DependencyInjection;
using SMDataAccess.Services;
using Microsoft.Extensions.Logging;
using SMDataAccess.Models.DataAccessModels;

namespace SMDataAccess;
public static class DependencyExtensions
{
    public static IServiceCollection AddDependencyServices(this IServiceCollection services, string defaultConn, LoggingSettings logSettings)
    {
        logSettings ??= new LoggingSettings();

        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<LoggingDbConnection>>();
            return new DbConnectionFactory(defaultConn, logger, logSettings);
        });

        services.AddTransient<IPartRepository, PartRepository>();

        return services;
    }
}
