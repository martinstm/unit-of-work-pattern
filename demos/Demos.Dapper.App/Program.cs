using Dapper.Core;
using Demos.Dapper.App;
using Demos.Dapper.App.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        services.AddUnitOfWork(configuration);
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
