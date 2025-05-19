using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using twitter.api.application.Services.Abstractions;
using twitter.api.application.Services;
using twitter.api.data.DbContexts;
using Microsoft.OpenApi.Models;
using System;

namespace twitter.api.web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTwitterServices(this IServiceCollection services)
        {
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();

            return services;
        }

        public static IServiceCollection AddTwitterDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ITwitterApiDbContext, TwitterApiDbContext>(options =>
                options.UseNpgsql(
                    config.GetConnectionString("TwitterDbContext"),
                    b => b.MigrationsAssembly("twitter.api.web"))
                );

            return services;
        }

        public static IServiceCollection AddTwitterAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var coso = config["Audience"];

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = config["Issuer"],
                        ValidAudience = config["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["Secret"]!)
                        )
                    };
                });

            return services;
        }

        public static IServiceCollection AddTwitterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce el token JWT con el formato: Bearer {token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Twitter API",
                    Version = "v1"
                });
            });

            return services;
        }   
    }
}
