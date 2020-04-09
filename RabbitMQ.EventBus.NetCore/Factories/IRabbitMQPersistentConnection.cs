using RabbitMQ.Client;
using RabbitMQ.EventBus.NetCore.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.EventBus.NetCore.Factories
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        RabbitMQEventBusConnectionConfiguration Configuration { get; }
        string Endpoint { get; }
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
