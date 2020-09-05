using Messages;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitClient
{
    public interface IEndpoint
    {
        Task Publish(RabbitMessage request, string message);
        //void Subscribe<T>( string topic, TMessage)
        Task Start(CancellationToken token);
    }
}
