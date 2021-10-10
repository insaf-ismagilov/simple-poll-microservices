﻿using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace SimplePoll.Common.RabbitMq.Subscribers
{
    public interface IRabbitMqSubscriber : IDisposable
    {
        void Subscribe(Func<BasicDeliverEventArgs, Task<bool>> func);
    }
}