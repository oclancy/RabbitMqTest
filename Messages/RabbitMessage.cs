using ProtoBuf;

namespace Messages
{
    [ProtoContract]
    [ProtoInclude(1, typeof(ReceiptMessage))]
    [ProtoInclude(2, typeof(OrderMessage))]
    public class RabbitMessage : IMessageBase
    {
        [ProtoMember(1)]
        public string PublishTopic {get;set;}
    }
}
