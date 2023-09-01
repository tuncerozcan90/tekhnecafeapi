using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Text.Json;
using TekhneCafe.Api.Extensions;
using TekhneCafe.Api.LoggerEnrichers;
using TekhneCafe.Business.Extensions;
using TekhneCafe.Core.Exceptions;
using TekhneCafe.Core.Extensions;
using TekhneCafe.DataAccess.Extensions;
using TekneCafe.SignalR.Extensions;
using TekneCafe.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

#region Serilog Configuration
SqlColumn sqlColumn = new SqlColumn();
sqlColumn.ColumnName = "UserId";
sqlColumn.DataType = System.Data.SqlDbType.UniqueIdentifier;
sqlColumn.PropertyName = "UserId";
sqlColumn.AllowNull = true;

ColumnOptions columnOpt = new ColumnOptions();
columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

using var log = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.With(new UserEnricher())
            .MinimumLevel.Warning()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("EfTekhneCafeContext"),
                sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true },
                columnOptions: columnOpt
                )
            .CreateLogger();
builder.Host.UseSerilog(log);
#endregion

builder.Services.AddApiServices();
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddSignalRServices();

var allowedHosts = builder.Configuration.GetValue<string>("AllowedHosts");

#region Cors Configurations
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
#endregion

var app = builder.Build();

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
                    _ => StatusCodes.Status500InternalServerError
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

app.Use(async (context, next) =>
{
    if (context.User?.Identity?.IsAuthenticated != null)
        LogContext.PushProperty("UserId", context.User.ActiveUserId());

    await next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<OrderNoficationHub>("/orderNotificationHub");
});

app.Run();
