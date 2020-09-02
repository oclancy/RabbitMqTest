using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    [ProtoContract]
    public class OrderMessage : RabbitMessage
    {
        [ProtoMember(100)]
        public string OrderRef { get; set; }
    }
}
