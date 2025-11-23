using Demos.RepoDB.App;
using Demos.RepoDB.App.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepoDb.Core;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        services.AddSqlCore(configuration);
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
