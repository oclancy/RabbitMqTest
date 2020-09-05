using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitClient
{
    public interface IConfigureAnEndpoint
    {
        IEnumerable<PublishDefinition> GetPublishDefinitions();
        IEnumerable<SubscriptionDefinition> GetSubscriptionDefinitions();
    }
}
