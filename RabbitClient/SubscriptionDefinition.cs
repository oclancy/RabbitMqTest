using System;

namespace RabbitClient
{
    public class SubscriptionDefinition
    {
        public Type Handler { get; set; }
        public Type MessageType { get; set; }
    }
}