using Microsoft.Extensions.Logging;
using SMDataAccess.Models.DataAccessModels;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace SMDataAccess.Services;
public class LoggingDbCommand : DbCommand
{
    private readonly DbCommand _innerCommand;
    private readonly ILogger _logger;
    private readonly string _logCategory;
    private readonly LoggingSettings _settings;
    public LoggingDbCommand(DbCommand innerCommand, ILogger logger, LoggingSettings settings, string logCategory = "SQL")
    {
        _innerCommand = innerCommand ?? throw new ArgumentNullException(nameof(innerCommand));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logCategory = logCategory;
        _settings = settings;
    }
    private void LogCommand(long elapsedMilliseconds)
    {
        if (_settings.LogSqlScripts)
        {
            var parameterCount = Parameters.Count;
            StringBuilder parameters = new();
            for(int i = 0; i < parameterCount; i++)
            {
                parameters.Append($"{Parameters[i].ParameterName}-{Parameters[i].DbType}-{Parameters[i].Value};");
            }

            _logger.Log(LogLevel.Information, new EventId(0, _logCategory),
                "Executed in: {elapsedMilliseconds} Ms. Parameters {parameters} SQL: {CommandText}", elapsedMilliseconds, parameters.ToString(), CommandText);
        }
        else
        {
            _logger.Log(LogLevel.Information, new EventId(0, _logCategory),
                "Executed in: {elapsedMilliseconds} Ms.", elapsedMilliseconds);
        }
    }
    public override string CommandText
    {
        get => _innerCommand.CommandText;
        set => _innerCommand.CommandText = value;
    }
    public override int CommandTimeout
    {
        get => _innerCommand.CommandTimeout;
        set => _innerCommand.CommandTimeout = value;
    }
    public override CommandType CommandType
    {
        get => _innerCommand.CommandType;
        set => _innerCommand.CommandType = value;
    }
    protected override DbConnection? DbConnection
    {
        get => _innerCommand.Connection;
        set => _innerCommand.Connection = value;
    }
    protected override DbParameterCollection DbParameterCollection => _innerCommand.Parameters;
    protected override DbTransaction? DbTransaction
    {
        get => _innerCommand.Transaction;
        set => _innerCommand.Transaction = value;
    }
    public override bool DesignTimeVisible
    {
        get => _innerCommand.DesignTimeVisible;
        set => _innerCommand.DesignTimeVisible = value;
    }
    public override UpdateRowSource UpdatedRowSource
    {
        get => _innerCommand.UpdatedRowSource;
        set => _innerCommand.UpdatedRowSource = value;
    }
    public override void Cancel()
    {
        _innerCommand.Cancel();
    }
    public override int ExecuteNonQuery()
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return _innerCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing non-query command.");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            LogCommand(stopwatch.ElapsedMilliseconds);
        }
    }
    public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await _innerCommand.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing non-query command asynchronously.");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            LogCommand(stopwatch.ElapsedMilliseconds);
        }
    }
    public override object ExecuteScalar()
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return _innerCommand.ExecuteScalar();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing scalar command.");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            LogCommand(stopwatch.ElapsedMilliseconds);
        }
    }
    public override async Task<object?> ExecuteScalarAsync(CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await _innerCommand.ExecuteScalarAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing scalar command asynchronously.");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            LogCommand(stopwatch.ElapsedMilliseconds);
        }
    }
    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return _innerCommand.ExecuteReader(behavior);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing data reader command.");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            LogCommand(stopwatch.ElapsedMilliseconds);
        }
    }
    protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await _innerCommand.ExecuteReaderAsync(behavior, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing data reader command asynchronously.");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            LogCommand(stopwatch.ElapsedMilliseconds);
        }
    }
    public override void Prepare()
    {
        _innerCommand.Prepare();
    }
    protected override DbParameter CreateDbParameter()
    {
        return _innerCommand.CreateParameter();
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _innerCommand.Dispose();
        }
        base.Dispose(disposing);
    }
}