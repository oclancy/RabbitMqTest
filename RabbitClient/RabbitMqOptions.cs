using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitClient
{
    public class RabbitMqOptions
    {
        public const string RabbitMqOptionsName = "RabbitMqOptions";

        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Queuename { get; set; }
    }

}
