using Interfaces;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> Logger;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IEnumerable<IRunAtStartup> startup)
        {
            Logger = logger;
            ServiceProvider = serviceProvider;
            Startup = startup;
        }

        public IServiceProvider ServiceProvider { get; }
        public IEnumerable<IRunAtStartup> Startup { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Starting");
            var endpoint = ServiceProvider.GetRequiredService<IEndpoint>();
            await endpoint.Start(stoppingToken);

            var mediator = ServiceProvider.GetService<MyMediator>();

            mediator.Initialize();

            Logger.LogInformation("Starting");
            foreach( var startup in Startup) await startup.OnStart(ServiceProvider);
        }
    }
}
