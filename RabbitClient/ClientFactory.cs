
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitClient
{
    public class ClientFactory : IDisposable
    {
        private readonly ILogger<ClientFactory> Logger;

        public ClientFactory(ILogger<ClientFactory> logger, RabbitMqOptions options)
        {
            Logger = logger;
            var factory = new ConnectionFactory()
            {
                HostName = options.Hostname,
                UserName = options.Username,
                Password = options.Password,
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
            factory.DispatchConsumersAsync = true;

            Logger.LogInformation($"Creating connection with {options.Hostname}, username {options.Username}");
            Connection = factory.CreateConnection();
        }

        private IConnection Connection { get; set; }
        
        public IModel GetChannel()
        {
           Logger.LogInformation("Creating channel");
           return Connection.CreateModel();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Connection.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Client()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
