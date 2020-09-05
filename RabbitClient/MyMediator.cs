using Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Description;
using System.Threading.Tasks;

namespace RabbitClient
{
    public class MyMediator
    {
        public MyMediator(ILogger<MyMediator> logger, IServiceProvider serviceProvider, IEnumerable<Type> handlerTypes)
        {
            ServiceProvider = serviceProvider;
            Logger = logger;
            HandlerTypes = handlerTypes;
        }

        readonly Dictionary<Type, List<Func<IMessageBase, Task>>> MessageHandlerMap = new Dictionary<Type, List<Func<IMessageBase, Task>>>();
        readonly Dictionary<Type, object> HandlerInstances = new Dictionary<Type, object>();

        public ILogger<MyMediator> Logger { get; }
        public IServiceProvider ServiceProvider { get; }
        public IEnumerable<Type> HandlerTypes { get; }

        public void Initialize()
        {
            foreach (var handler in HandlerTypes)
            {
                Add(handler);
            }
        }

        public void Add(Type handler)
        {
            var handleMethods = handler.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                  .Where(info => info.Name == "Handle"
                                 && info.GetParameters().Length == 1
                                 && typeof(IMessageBase).IsAssignableFrom(info.GetParameters().First().ParameterType));

            foreach (var method in handleMethods)
            {
                var messageType = method.GetParameters().First();
                if (!MessageHandlerMap.ContainsKey(messageType.ParameterType))
                {
                    MessageHandlerMap[messageType.ParameterType] = new List<Func<IMessageBase, Task>>();
                }

                if (!HandlerInstances.ContainsKey(handler))
                {
                    //HandlerInstances[handler] = ServiceProvider.GetService(handler);
                    //var constructor = handler.GetConstructor(new[] { typeof(ILogger<Publisher>), typeof(IConfigureAnEndpoint), typeof(IEndpoint) });
                    //var constructor = handler.GetConstructors().First();
                    //var constructorParams =
                    //constructor.GetParameters().Select(info =>
                    //    ServiceProvider.GetRequiredService(info.ParameterType)
                    //).ToArray<object>();

                    //var configurator = ServiceProvider.GetService<IConfigureAnEndpoint>();
                    var obj = ActivatorUtilities.CreateInstance(ServiceProvider, handler);
                    //var obj = constructor.Invoke(constructorParams);
                    HandlerInstances[handler] = obj;
                }

                CreateDelegate(handler, method, messageType.ParameterType);

            }
        }

        public void Add<TMessage>(Type handler) where TMessage : IMessageBase
        {
            if (!MessageHandlerMap.ContainsKey(typeof(TMessage)))
            {
                MessageHandlerMap[typeof(TMessage)] = new List<Func<IMessageBase, Task>>();
            }

            if (!HandlerInstances.ContainsKey(handler))
            {
                HandlerInstances[handler] = ServiceProvider.GetService(handler);
            }

            var method = handler.GetMethod("Handle",
                            BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                            null,
                            new[] { typeof(TMessage) },
                            null);

            CreateDelegate(handler, method, typeof(TMessage));

        }

        private void CreateDelegate(Type handler, MethodInfo method, Type messageType)
        {
            if (method == null)
            {
                Logger.LogError($"Handle method on type {handler} for MessageType {messageType} not found");
                return;
            }
            var handlerInstance = HandlerInstances[handler];

            var delegateListForMessageType = MessageHandlerMap[messageType];

            delegateListForMessageType.Add(async (message) =>
            {
                await (Task)method.Invoke(handlerInstance, new[] { message });
            });
        }


        public async Task Dispatch(IMessageBase message)
        {
            if (message == null) return;

            Logger.LogInformation($"Dispatching {message}");

            var currentType = message.GetType();
            do
            {
                if (MessageHandlerMap.ContainsKey(currentType))
                {
                    var dispatchDelegates = MessageHandlerMap[currentType];

                    foreach (var handler in dispatchDelegates)
                    {
                        try
                        {
                            await handler(message);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                currentType = currentType.BaseType;
            }
            while (currentType != null);
        }

    }
}
