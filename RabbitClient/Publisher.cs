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
            // gettopic
            Logger.LogInformation($"Publishing {request}");
            var destination = Configuration.GetPublishDefinitions()
                                           .FirstOrDefault( p => p.MessageType==request.GetType());

            request.PublishTopic = destination?.Topic;

            try
            {
                await Endpoint.Publish(request);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Message not sent: {request}");
            }
        }
    }
}
