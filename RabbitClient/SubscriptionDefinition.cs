using System;

namespace RabbitClient
{
    public class SubscriptionDefinition
    {
        public string Topic { get; set; }
        public Type Handler { get; set; }
        public Type MessageType { get; set; }
    }
}