using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PortalIpalEscalas.API.Config;
using System.Collections.Generic;
using System;
using System.Text;

namespace PortalIpalEscalas.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyMethod()
                          .AllowAnyHeader()
                          .SetIsOriginAllowed(_ => true)
                          .AllowCredentials();
                });
            });

            services.AddInfra(configuration);
            services.AddHealthChecks();

            services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("Authentication", new OpenApiSecurityScheme
                {
                    Description = "Header de Autorization JWT usando o esquema Bearer. Ex: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "auth"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "auth"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            var host = "https://apiescalasipal-client.com.br";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                var validation = bearerOptions.TokenValidationParameters;
                var key = new SymmetricSecurityKey(Encoding.Default.GetBytes(configuration["JwtKey"]));
                validation.IssuerSigningKey = key;
                validation.ValidAudience = host;
                validation.ValidIssuer = host;
                validation.ValidateIssuerSigningKey = true;
                validation.ValidateLifetime = true;
                validation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorizationBuilder().AddPolicy("Bearer", policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                      .RequireAuthenticatedUser();
            });

            return services;
        }
    }
}
