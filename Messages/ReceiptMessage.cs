using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    [ProtoContract]
    public class ReceiptMessage : RabbitMessage
    {
        [ProtoMember(100)]
        public string OrderRef { get; set; }
        [ProtoMember(101)] 
        public string ReceiptId { get; set; }
    }
}
