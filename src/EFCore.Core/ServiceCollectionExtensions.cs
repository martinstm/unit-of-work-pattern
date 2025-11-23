using Configuration;
using EFCore.Core.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlCore<T>(this IServiceCollection services, IConfiguration configuration)
        where T: DbContext
        {
            services.AddDbConfig(configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork<T>>();

            return services;
        }
    }
}
