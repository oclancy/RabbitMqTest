using Interfaces;


using Messages;

using Microsoft.Extensions.DependencyInjection;
using RabbitClient;
using System;

namespace ExampleProducer
{
    public class Startup : IRunAtStartup
    {
        public void OnStart(IServiceProvider serviceProvider)
        {
            var mediator= serviceProvider.GetService<MyMediator>();
            mediator.Dispatch(new OrderMessage());
        }
    }
}
