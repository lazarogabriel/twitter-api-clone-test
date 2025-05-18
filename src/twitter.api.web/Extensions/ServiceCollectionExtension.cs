using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using twitter.api.data.DbContexts;

namespace twitter.api.web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTwitterDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TwitterApiDbContext>(options =>
                options.UseNpgsql(
                    config.GetConnectionString("TwitterDbContext"),
                    b => b.MigrationsAssembly("twitter.api.web"))
                );

            return services;
        }

        public static IServiceCollection AddTwitterAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
                        )
                    };
                });

            return services;
        }
    }
}
