using MediatR;
using ProtoBuf;
using System;

namespace Messages
{
    [ProtoContract]
    [ProtoInclude(1, typeof(RabbitMessage))]
    public interface IMessageBase : IRequest, INotification
    {
    }
}
