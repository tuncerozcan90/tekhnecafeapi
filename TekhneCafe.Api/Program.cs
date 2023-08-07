using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using TekhneCafe.Api.Extensions;
using TekhneCafe.Business.Extensions;
using TekhneCafe.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Serilog Configuration
using var log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("EfTekhneCafeContext"),
                sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true }
                )
            .CreateLogger();
builder.Host.UseSerilog(log);
#endregion

builder.Services.AddApiServices();
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
