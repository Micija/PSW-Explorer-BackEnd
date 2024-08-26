using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PSW24.API.DTOs;
using System.Text;
using System.Text.Json;

namespace PSW24_BackEnd.Startup
{
    public static class AuthConfiguration
    {

        public static IServiceCollection ConfigureAuth(this IServiceCollection services)
        {
            ConfigureAuthentication(services);
            ConfigureAuthorizationPolicies(services);
            return services;
        }

        private static void ConfigureAuthentication(IServiceCollection services)
        {
            string filePath = "Resources/JWT.json";
            string jsonString = File.ReadAllText(filePath);
            JWTDto credentials = JsonSerializer.Deserialize<JWTDto>(jsonString);

            var key = credentials.Key;
            var issuer = credentials.Issuer;
            var audience = credentials.Audience;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("AuthenticationTokens-Expired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        private static void ConfigureAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("waiterPolicy", policy => policy.RequireRole("author"));
                options.AddPolicy("clientPolicy", policy => policy.RequireRole("admin"));
                options.AddPolicy("clientPolicy", policy => policy.RequireRole("tourist"));
            });
        }

    }
}
