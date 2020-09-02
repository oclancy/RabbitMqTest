using Messages;
using RabbitClient;
using System;
using System.Collections.Generic;

namespace ExampleProducer
{
    public class Configurator : IConfigureAnEndpoint
    {
        public IEnumerable<PublishDefinition> ConfigurePublisher()
        {
            yield return new PublishDefinition{ Topic = "orders", MessageType = typeof(OrderMessage) };
            yield break;
        }

        public IEnumerable<SubscriptionDefinition> ConfigureSubscriptions()
        {
            yield return new SubscriptionDefinition { Handler = typeof(ReceiptMessageHandler), MessageType = typeof(ReceiptMessage) };
            yield break;
        }
    }
}
