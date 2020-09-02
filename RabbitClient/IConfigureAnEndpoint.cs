using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitClient
{
    public interface IConfigureAnEndpoint
    {
        IEnumerable<PublishDefinition> ConfigurePublisher();
        IEnumerable<SubscriptionDefinition> ConfigureSubscriptions();
    }
}
