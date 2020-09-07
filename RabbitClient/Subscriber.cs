
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
        public Subscriber(ILogger<Subscriber> logger, IConfigureAnEndpoint configuration, IEndpoint endpoint)
        {
            Logger = logger;
            Configuration = configuration;
            Endpoint = endpoint;

            endpoint.OnMessage += Endpoint_OnMessage;
        }

        private async void Endpoint_OnMessage(object sender, RabbitMessage e)
        {
            if (Mediator == null)
                return;

            Logger.LogInformation($"Dispatching {e}");
            await Mediator.Dispatch(e);
        }

        public ILogger<Subscriber> Logger { get; }
        public IConfigureAnEndpoint Configuration { get; }
        public IEndpoint Endpoint { get; }
        public MyMediator Mediator { get; set;  }

        public async Task Dispatch(RabbitMessage message)
        {
            Logger.LogInformation($"Dispatching {message}");
            await Mediator.Dispatch(message);
        }
    }
}
