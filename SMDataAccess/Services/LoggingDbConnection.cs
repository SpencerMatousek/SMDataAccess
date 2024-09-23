using Microsoft.Extensions.Logging;
using SMDataAccess.Models.DataAccessModels;
using System.Data;
using System.Data.Common;

namespace SMDataAccess.Services;
public class LoggingDbConnection : DbConnection
{
    private readonly DbConnection _inner;
    private readonly ILogger<LoggingDbConnection> _logger;
    private readonly LoggingSettings _settings;
    public LoggingDbConnection(DbConnection innerConnection, ILogger<LoggingDbConnection> logger, LoggingSettings settings)
    {
        _inner = innerConnection ?? throw new ArgumentNullException(nameof(innerConnection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settings = settings;
    }
    public override string ConnectionString
    {
        get => _inner.ConnectionString;
        set => _inner.ConnectionString = value;
    }
    public override string Database => _inner.Database;
    public override string DataSource => _inner.DataSource;
    public override string ServerVersion => _inner.ServerVersion;
    public override ConnectionState State => _inner.State;
    public override void ChangeDatabase(string databaseName) => _inner.ChangeDatabase(databaseName);
    public override void Close() => _inner.Close();
    public override void Open()
    {
        _inner.Open();
        _logger.LogInformation("Connection Opened");
    }
    public override async Task OpenAsync(CancellationToken cancellationToken)
    {
        await _inner.OpenAsync(cancellationToken);
        _logger.LogInformation("Connection Opened Async");
    }
    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
        return _inner.BeginTransaction(isolationLevel);
    }
    protected override DbCommand CreateDbCommand()
    {
        var command = _inner.CreateCommand();
        return new LoggingDbCommand(command, _logger, _settings);
        //return command;
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _inner.Dispose();
        }
        base.Dispose(disposing);
    }
    public override void EnlistTransaction(System.Transactions.Transaction transaction)
    {
        _inner.EnlistTransaction(transaction);
    }
}