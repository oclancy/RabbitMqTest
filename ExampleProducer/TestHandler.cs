using Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleProducer
{
    public class TestHandler : IAmAMessageHandler<OrderMessage>
    {
        public Task Handle(OrderMessage request)
        {
            throw new NotImplementedException();
        }
    }
}
