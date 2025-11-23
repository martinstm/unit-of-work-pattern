using Demos.RepoDB.App.Domain;
using Demos.RepoDB.App.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RepoDb;
using RepoDb.Core.UnitOfWorkPattern;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demos.RepoDB.App
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_unitOfWork.BeginTransaction();

            FluentMapper
                .Entity<User>() // Define which Class or Model
                .Table("[dbo].[User]") // Map the Class/Table
                .Primary(e => e.Id) // Define the Primary
                .Identity(e => e.Id) // Define the Identity
                .Column(e => e.Name, "[Name]") // Map the Property/Column
                .Column(e => e.Email, "[Email]"); // Map the Property/Column

            try
            {

                //await _userRepository.InsertAsync(new User
                //{
                //    Id = Guid.NewGuid(),
                //    Name = "User Test",
                //    Email = "test@mail.com"
                //});
                var result = await _userRepository.GetAllAsync();
                _unitOfWork.Commit();
            }
            catch
            {
                //_unitOfWork.Rollback();
            }


        }
    }
}