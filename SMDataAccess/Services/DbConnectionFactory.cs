using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMDataAccess.Models.DataAccessModels;

namespace SMDataAccess.Services;
public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    private readonly ILogger<LoggingDbConnection> _logger;
    private readonly LoggingSettings _settings;

    public DbConnectionFactory(string connectionString, ILogger<LoggingDbConnection> logger, LoggingSettings logSettings)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _logger = logger;
        _settings = logSettings;
    }

    public IDbConnection CreateConnection()
    {
        var sqlConnection = new SqlConnection(_connectionString);
        return new LoggingDbConnection(sqlConnection, _logger, _settings);
    }
}