
using Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitClient
{
    public class Subscriber
    {
        public Subscriber(ILogger<Subscriber> logger, IConfigureAnEndpoint configuration, MyMediator mediator)
        {
            Logger = logger;
            Configuration = configuration;
            Mediator = mediator;
        }

        public ILogger<Subscriber> Logger { get; }
        public IConfigureAnEndpoint Configuration { get; }
        public MyMediator Mediator { get; }

        public async Task Dispatch(RabbitMessage message)
        {
            Logger.LogInformation($"Dispatching {message}");
            await Mediator.Dispatch(message);
        }
    }
}
