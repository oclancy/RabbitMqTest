using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Interfaces;
using Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ProtoBuf.Meta;
using RabbitClient;

namespace RabbitService
{
    public class Program
    {
       public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging( builder => {
                    builder.AddNLog();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var assemblies = Directory.GetFiles(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName, "*.dll")
                                        .Select(file => Assembly.LoadFile(file))
                                        .ToArray();

                    services.AddSingleton(hostContext.Configuration.GetSection("RabbitMqOptions").Get<RabbitMqOptions>());

                    var startupTypes = assemblies.GetTypesImplementingInterface(typeof(IRunAtStartup));
                    var configuratorType = assemblies.GetTypesImplementingInterface(typeof(IConfigureAnEndpoint)).FirstOrDefault();
                    var handlerTypes = assemblies.GetTypesImplementingGenericInterface(typeof(IAmAMessageHandler<>));

                    
                    services.AddSingleton(typeof(IConfigureAnEndpoint), configuratorType);
                    services.AddSingleton((sp) => startupTypes.Select( type => Activator.CreateInstance(type) as IRunAtStartup)) ;
                    services.AddSingleton<IEndpoint, RabbitEndpoint>();
                    services.AddSingleton<MyMediator>();

                    services.AddSingleton<ClientFactory>();

                    services.AddSingleton<Subscriber>();

                    services.AddSingleton<Publisher>();


                    foreach (var type in handlerTypes)
                    {
                        services.AddSingleton(typeof(IAmAMessageHandler), type);
                    }
                    services.AddHostedService<Worker>();

                });
    }
}
