using Messages;
using RabbitClient;
using System;
using System.Collections.Generic;

namespace ExampleConsumer
{
    public class Configurator : IConfigureAnEndpoint
    {
        public Configurator() { }

        public IEnumerable<PublishDefinition> GetPublishDefinitions()
        {
            yield return new PublishDefinition{ Topic = Topics.OrderResponse, MessageType = typeof(ReceiptMessage) };
            yield break;
        }

        public IEnumerable<SubscriptionDefinition> GetSubscriptionDefinitions()
        {
            yield return new SubscriptionDefinition { Topic = Topics.OrderRequest, Handler = typeof(OrderMessageHandler), MessageType = typeof(OrderMessage) };
            yield break;
        }
    }
}
