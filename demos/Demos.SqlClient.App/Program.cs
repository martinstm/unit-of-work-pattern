using Configuration;
using Demos.SqlClient.App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        services.AddDbConfig(configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
