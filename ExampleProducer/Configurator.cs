using Messages;
using RabbitClient;
using System;
using System.Collections.Generic;

namespace ExampleProducer
{
    public class Configurator : IConfigureAnEndpoint
    {
        public Configurator() { }

        public IEnumerable<PublishDefinition> GetPublishDefinitions()
        {
            yield return new PublishDefinition{ Topic = "orders", MessageType = typeof(OrderMessage) };
            yield break;
        }

        public IEnumerable<SubscriptionDefinition> GetSubscriptionDefinitions()
        {
            yield return new SubscriptionDefinition { Handler = typeof(ReceiptMessageHandler), MessageType = typeof(ReceiptMessage) };
            yield break;
        }
    }
}
