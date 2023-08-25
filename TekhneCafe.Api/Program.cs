using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json;
using TekhneCafe.Api.Extensions;
using TekhneCafe.Business.Extensions;
using TekhneCafe.Core.Exceptions;
using TekhneCafe.DataAccess.Extensions;
using TekneCafe.SignalR.Extensions;
using TekneCafe.SignalR.Hubs;

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
builder.Services.AddSignalRServices();

var allowedHosts = builder.Configuration.GetValue<string>("AllowedHosts");
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins(allowedHosts) // Burada eriþime izin vermek istediðiniz orijinleri belirtin
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(_ =>
    {
        _.SwaggerEndpoint("/swagger/v1/swagger.json", "TekhneCafe API v1");
    });
}

app.UseCors("_myAllowSpecificOrigins");

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
                    BadRequestException ex => StatusCodes.Status400BadRequest,
                    NotFoundException ex => StatusCodes.Status404NotFound,
                    ForbiddenException ex => StatusCodes.Status403Forbidden,
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

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<OrderNoficationHub>("/myhub");
});

app.Run();
