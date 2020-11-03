using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PS.Template.JWSToken
{
    public static class Authentication
    {
        public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/pstemplate";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/pstemplate",
                        ValidateAudience = true,
                        ValidAudience = "pstemplate",
                        ValidateLifetime = true
                    };
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(configuration.GetSection("Authentication:SecretKey").Value)),
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateAudience = false
                    };
                });
            return services;
        }

        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration Authentication
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Autenticacion:SecretKey").Value);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
                    {
                        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                        {
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes("Lima-Villajuan-Djirikian-Blasi-Gargatagli")
                                ),

                            ValidateIssuer = false,
                            ValidateAudience = false,
                            RequireExpirationTime = false
                        };
                    });
            return services;
        }
    }
}