using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Messages
{
    public static class RabbitMessageExtensions
    {
        public static byte[] Serialize(this RabbitMessage message)
        {
            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, message);
                return stream.ToArray();
            }
        }

        public static RabbitMessage Deserialize(byte[] data)
        {
            return ProtoBuf.Serializer.Deserialize<RabbitMessage>(new ReadOnlyMemory<byte>(data));
        }
    }
}
