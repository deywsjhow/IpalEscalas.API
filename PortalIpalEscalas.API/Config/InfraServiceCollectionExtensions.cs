using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalIpalEscalas.Infraestructure.Interfaces;
using PortalIpalEscalas.Domain;
using PortalIpalEscalas.Repository;

namespace PortalIpalEscalas.API.Config
{
    public static class InfraServiceCollectionExtensions
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountContext, AccountContext>();
        }
    }
}
