using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public static class Topics
    {
        public const string Order = "order";

        public static readonly string OrderResponse = string.Join(".", Order, "response");

        public static readonly string OrderRequest = string.Join(".", Order, "request");
    }
}
