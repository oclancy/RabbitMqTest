using Messages;

using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitClient
{
    public class RabbitEndpoint : IDisposable, IEndpoint
    {
        public event EventHandler<RabbitMessage> OnMessage;

        public RabbitEndpoint(ILogger<RabbitEndpoint> logger, 
            ClientFactory factory,
            RabbitMqOptions options) 
        {
            Logger = logger;
            QueueName = string.IsNullOrEmpty(options.Queuename)? options.Username : options.Queuename;
            Channel = factory.GetChannel();
            var result = Channel.QueueDeclare(queue: QueueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            if (result.QueueName != QueueName)
            {
                throw new ApplicationException($"Could not create Queue: {QueueName}");
            }

        }

        public Task Start(CancellationToken token)
        {
            Consumer = new AsyncEventingBasicConsumer(Channel);
            Consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...
                OnMessage(this, RabbitMessageExtensions.Deserialize(body));

                Channel.BasicAck(ea.DeliveryTag, false);
            };

            // this consumer tag identifies the subscription
            // when it has to be cancelled
            ConsumerTag = Channel.BasicConsume(QueueName, false, Consumer);

            return Task.CompletedTask;
        }


        public ILogger<RabbitEndpoint> Logger { get; private set; }
        public Subscriber Subscriber { get;  }
        public string QueueName { get; }
        public IModel Channel { get; private set; }
        public AsyncEventingBasicConsumer Consumer { get; private set; }
        public string ConsumerTag { get; private set; }

        public Task Publish(RabbitMessage message)
        {
            if (string.IsNullOrEmpty(message.PublishTopic)) return Task.FromException(new ArgumentException("Publish Topic not specified."));

            Channel.BasicPublish(exchange: "amq.topic",
                                 routingKey: message.PublishTopic,
                                 basicProperties: null,
                                 body: message.Serialize());

            Logger.LogInformation(" [x] Sent {0}", message);

            return Task.CompletedTask;
        }

        public void SubscribeTopic(string topic)
        {
            Channel.QueueBind(QueueName, "amq.topic", topic);
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Channel.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RabbitEndpoint()
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
