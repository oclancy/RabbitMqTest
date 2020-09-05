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
            try
            {
                using (var stream = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize(stream, message);
                    return stream.GetBuffer();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static RabbitMessage Deserialize(byte[] data)
        {
            try
            {
                using (var stream = new MemoryStream(data))
                {
                    return ProtoBuf.Serializer.Deserialize<RabbitMessage>(stream);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
