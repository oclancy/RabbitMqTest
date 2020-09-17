using Interfaces;


using Messages;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitClient;
using System;
using System.Threading.Tasks;

namespace ExampleProducer
{
    public class Startup : IRunAtStartup
    {
        public Startup(ILogger<Startup> logger) {
            Logger = logger;
        }

        public ILogger<Startup> Logger { get; }

        public async Task OnStart(IServiceProvider serviceProvider)
        {
            
            var mediator= serviceProvider.GetService<MyMediator>();
            var msg = new OrderMessage { OrderRef ="100" };
            Logger.LogInformation($"Sending {msg}");
            await mediator.Dispatch(msg);
        }
    }
}
