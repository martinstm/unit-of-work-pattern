using Dapper.Core.RepositoryPattern;
using Dapper.Core.UnitOfWorkPattern;
using Demos.Dapper.App.Domain;
using Demos.Dapper.App.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demos.Dapper.App
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                try
                {
                    var existingUser = await unitOfWork.Get<IUserRepository>().GetByEmailAsync("john@mail.com");
                    var teams = await unitOfWork.Get<IRepository<Team>>().GetAllAsync();

                    var result = await unitOfWork.Get<IUserRepository>().GetAllAsync();

                    _logger.LogInformation($"All db users: " +
                        $"{string.Join(", ", result.Select(u => u.Email))}");

                    _logger.LogInformation($"All db teams: " +
                        $"{string.Join(", ", teams.Select(t => t.Name))}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"There was an exception during the transaction. " +
                                        $"Error message: {ex.Message}");
                }
            }
        }
    }
}