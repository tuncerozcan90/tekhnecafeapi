using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Concrete;

namespace TekhneCafe.Business.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            services.AddScoped<IAppRoleService, AppRoleManager>();
        }
    }
}
