using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PS.Template.JWSToken
{
    public static class Authentication
    {
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