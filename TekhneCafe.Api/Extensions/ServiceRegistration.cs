using Microsoft.AspNetCore.Mvc;

namespace TekhneCafe.Api.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddEndpointsApiExplorer();

            #region Swagger Configuration
            services.AddSwaggerGen();
            #endregion
        }
    }
}
