using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;

namespace TekhneCafe.DataAccess.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddDbContext<EfTekhneCafeContext>(_ => _.UseSqlServer(configurations.GetConnectionString("EfTekhneCafeContext")));
            services.AddScoped<IAppRoleDal, EfAppRoleDal>();
            services.AddScoped<IAppUserDal, EfAppUserDal>();
            services.AddScoped<ICartDal, EfCartDal>();
            services.AddScoped<ICartLineDal, EfCartLineDal>();
            services.AddScoped<IImageDal, EfImageDal>();
            services.AddScoped<INotificationDal, EfNotificationDal>();
            services.AddScoped<IOrderDal, EfOrderDal>();
            services.AddScoped<IOrderHistoryDal, EfOrderHistoryDal>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ITransactionHistoryDal, EfTransactionHistoryDal>();
            services.AddScoped<ITransactionTypeDal, EfTransactionTypeDal>();
        }
    }
}
