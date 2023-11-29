﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalIpalEscalas.Infraestructure.Interfaces;
using PortalIpalEscalas.Domain;
using PortalIpalEscalas.Repository;
using PortalIpalEscalas.API.Sender;

namespace PortalIpalEscalas.API.Config
{
    public static class InfraServiceCollectionExtensions
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthtContext, AuthContext>();
            services.AddScoped<IToken, Token>();
            services.AddScoped<IScaleContext, ScaleContext>();
            services.AddScoped<IScaleService, ScaleService>();
            services.AddScoped<SendMessage, SendMessage>();
        }
    }
}
