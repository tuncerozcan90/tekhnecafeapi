using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json;
using TekhneCafe.Api.Extensions;
using TekhneCafe.Business.Extensions;
using TekhneCafe.Core.Exceptions;
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
builder.Services.AddBusinessServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region ExceptionHandler Configuration
app.UseExceptionHandler(
    options =>
    {
        options.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

            if (exceptionObject != null)
            {
                context.Response.StatusCode = exceptionObject.Error switch
                {
                    BadRequestException exception => StatusCodes.Status400BadRequest,
                    NotFoundException exception => StatusCodes.Status404NotFound,
                    InternalServerErrorException exception => StatusCodes.Status500InternalServerError,
                    _ => 999999
                };
                var errorMessage = $"{exceptionObject.Error.Message}";
                await context.Response
                    .WriteAsync(JsonSerializer.Serialize(new
                    {
                        context.Response.StatusCode,
                        exceptionObject.Error.Message
                    }))
                    .ConfigureAwait(false);
            }
        });
    }
);
#endregion

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
