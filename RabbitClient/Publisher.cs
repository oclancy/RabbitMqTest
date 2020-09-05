using Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitClient
{
    public class Publisher : IAmAMessageHandler<RabbitMessage>
    {
        public Publisher(
            ILogger<Publisher> logger,
            IConfigureAnEndpoint configuration,
            IEndpoint endpoint)
        {
            Logger = logger;
            Configuration = configuration;
            Endpoint = endpoint;
        }

        public ILogger<Publisher> Logger { get; }
        public IConfigureAnEndpoint Configuration { get; }
        public IEndpoint Endpoint { get; }

        private string GetDestinationForMessage(RabbitMessage request)
        {
            return string.Empty;
        }

        public async Task Handle(RabbitMessage request )
        {
            // gettopic
            Logger.LogInformation($"Publishing {request}");
            var destination = GetDestinationForMessage(request);
            await Endpoint.Publish(request, destination);
        }
    }
}
