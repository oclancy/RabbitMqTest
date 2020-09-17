
using ProtoBuf;

namespace Messages
{
    [ProtoContract]
    [ProtoInclude(100, typeof(ReceiptMessage))]
    [ProtoInclude(101, typeof(OrderMessage))]
    public class RabbitMessage : IMessageBase
    {
        [ProtoMember(1)]
        public string PublishTopic {get;set;}
    }
}
