using Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleProducer
{
    public class ReceiptMessageHandler : IAmAMessageHandler<ReceiptMessage> //IAmAMessageHandler<ReceiptMessage>, 
    {
        public ReceiptMessageHandler(ILogger<ReceiptMessageHandler> logger) 
        {
            Logger = logger;
        }

        public ILogger<ReceiptMessageHandler> Logger { get; }

        public async Task Handle(ReceiptMessage message)
        {
            Logger.LogInformation($"Recieved {message}");
        }


    }
}
