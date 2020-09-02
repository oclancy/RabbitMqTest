using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
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
                    services.AddSingleton(hostContext.Configuration.GetSection("RabbitMqOptions").Get<RabbitMqOptions>());
                            
                    //services.AddSingleton((sp) => {
                    //    var options = hostContext.Configuration.Get<RabbitMqOptions>();
                    //    return new ClientFactory(options);
                    //});

                    services.AddSingleton<ClientFactory>();

                    services.AddTransient<IEndpoint, RabbitEndpoint>();

                    services.AddHostedService<Worker>();
                });
    }
}
