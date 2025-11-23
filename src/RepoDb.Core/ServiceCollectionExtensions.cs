using Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepoDb.Core.UnitOfWorkPattern;

namespace RepoDb.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbConfig(configuration);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            SqlServerBootstrap.Initialize();
            
            return services;
        }
    }
}
