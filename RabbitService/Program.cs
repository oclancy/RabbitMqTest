using Interfaces;

using Messages;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NLog.Extensions.Logging;

using RabbitClient;

using System.IO;
using System.Linq;
using System.Reflection;

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

                    services.AddSingleton(hostContext.Configuration.GetSection(RabbitMqOptions.RabbitMqOptionsName).Get<RabbitMqOptions>());

                    var startupTypes = assemblies.GetTypesImplementingInterface(typeof(IRunAtStartup));
                    var configuratorType = assemblies.GetTypesImplementingInterface(typeof(IConfigureAnEndpoint)).FirstOrDefault();
                    var handlerTypes = assemblies.GetTypesImplementingGenericInterface(typeof(IAmAMessageHandler<>));

                    
                    if(configuratorType != null)
                        services.AddSingleton(typeof(IConfigureAnEndpoint), configuratorType);
                    
                    //services.AddSingleton((sp) => startupTypes.Select( type => Activator.CreateInstance(type) as IRunAtStartup)) ;
                    foreach (var startupType in startupTypes)
                        services.AddSingleton(typeof(IRunAtStartup), startupType);

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
