using Messages;
using System;

namespace RabbitClient
{

    public class PublishDefinition
    {
        public string Topic { get; set; }

        public PublishType PublishType { get; set; } = PublishType.Queue;

        public Type MessageType { get; set; }
    }
}