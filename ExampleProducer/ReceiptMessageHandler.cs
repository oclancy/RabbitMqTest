using Messages;

using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace ExampleProducer
{
    public class ReceiptMessageHandler : IAmAMessageHandler<ReceiptMessage>
    {
        public ReceiptMessageHandler(ILogger<ReceiptMessageHandler> logger) 
        {
            Logger = logger;
        }

        public ILogger<ReceiptMessageHandler> Logger { get; }

        public Task Handle(ReceiptMessage message)
        {
            Logger.LogInformation($"Recieved {message}");
            return Task.CompletedTask;
        }


    }
}
