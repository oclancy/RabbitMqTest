using Messages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExampleProducer
{
    public class TestHandler : IAmAMessageHandler<OrderMessage>
    {
        public TestHandler(ILogger<TestHandler> logger)
        {
            Logger = logger;
        }

        public ILogger<TestHandler> Logger { get; }

        public Task Handle(OrderMessage request)
        {
            Logger.LogInformation($"Handling {request}");
            return Task.CompletedTask;
        }
    }
}
