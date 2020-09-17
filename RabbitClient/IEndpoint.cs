using Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitClient
{
    public interface IEndpoint
    {
        event EventHandler<RabbitMessage> OnMessage;

        Task Publish(RabbitMessage request);
        Task Start(CancellationToken token);
        void SubscribeTopic(string topic);
    }
}
