using Messages;

using Microsoft.Extensions.Logging;

using RabbitClient;

using System.Threading.Tasks;

namespace ExampleConsumer
{
    public class OrderMessageHandler : IAmAMessageHandler<OrderMessage>
    {
        public OrderMessageHandler(ILogger<OrderMessageHandler> logger, MyMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public ILogger<OrderMessageHandler> Logger { get; }
        public MyMediator Mediator { get; }

        public async Task Handle(OrderMessage request)
        {
            Logger.LogInformation($"Handling {request}");
            await Mediator.Dispatch( new ReceiptMessage());
        }
    }
}
