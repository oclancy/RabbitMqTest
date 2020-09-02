﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public interface IAmAMessageHandler<T> where T:RabbitMessage
    {
        Task Handle(T message);
    }
}
