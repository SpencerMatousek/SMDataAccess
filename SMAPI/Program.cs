using Serilog;
using Serilog.Events;
using SMDataAccess;
using SMDataAccess.Models.DataAccessModels;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var loggerConfig = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console();

if (env.IsDevelopment())
{
    loggerConfig = loggerConfig.WriteTo.Logger(lc => lc
        //.Filter.ByIncludingOnly(evt => evt.Properties.ContainsKey("Category") && evt.Properties["Category"].ToString().Contains("SQL"))
        .WriteTo.File(
            path: "Logs/sql_logs-.log",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Information,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
        )
    );
}

Log.Logger = loggerConfig.CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddLogging(config =>
{
    config.AddDebug();
});

var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException();
var logSettings = builder.Configuration.GetSection("LoggingSettings").Get<LoggingSettings>();

builder.Services.AddDependencyServices(defaultConnection, logSettings ?? new LoggingSettings());

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
