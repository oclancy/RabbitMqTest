using Messages;

using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitClient
{
    public class Publisher : IAmAMessageHandler<RabbitMessage>
    {
        public Publisher(
            ILogger<Publisher> logger,
            IConfigureAnEndpoint configuration)
        {
            Logger = logger;
            Configuration = configuration;
        }

        public ILogger<Publisher> Logger { get; }
        public IConfigureAnEndpoint Configuration { get; }
        public IEndpoint Endpoint { get; set; }

        public async Task Handle(RabbitMessage request )
        {
            // get topic
            var destination = Configuration.GetPublishDefinitions()
                                           .FirstOrDefault( p => p.MessageType==request.GetType());

            if (destination == null) return;

            request.PublishTopic = destination?.Topic;

            try
            {
                Logger.LogInformation($"Publishing {request}");
                await Endpoint.Publish(request);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Message not sent: {request}");
            }
        }
    }
}
