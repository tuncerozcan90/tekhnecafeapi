using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Concrete;

namespace TekhneCafe.Business.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region jwt Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateAudience = true,//olusturulacak token degerini kimlerin/hangi originlerin erisebilecegi
                        ValidateIssuer = true,  //tokeni olusturan
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = configuration.GetSection("JWTAuth:ValidAudienceURL").Value,
                        ValidIssuer = configuration.GetSection("JWTAuth:ValidIssuerURL").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTAuth:SecretKey").Value)),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

                        NameClaimType = ClaimTypes.Name
                    };
                });
            #endregion

            #region IOC Scoped Services
            services.AddScoped<ITokenService, JwtManager>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAuthenticationService, LdapAuthenticationManager>();
            #endregion
        }
    }
}
