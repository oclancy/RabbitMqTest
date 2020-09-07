using Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitClient
{
    public interface IEndpoint
    {
        event EventHandler<RabbitMessage> OnMessage;

        Task Publish(RabbitMessage request, string message);
        //void Subscribe<T>( string topic, TMessage)
        Task Start(CancellationToken token);
    }
}
