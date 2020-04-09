using RabbitMQ.Client;
using RabbitMQ.EventBus.NetCore.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.EventBus.NetCore
{
    public interface IRabbitMQEventBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        void Publish<TMessage>(TMessage message, string exchange, string routingKey, string type = ExchangeType.Topic);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="type"></param>
        void Subscribe<TEvent, THandler>(string type = ExchangeType.Topic)
            where TEvent : IEvent
            where THandler : IEventHandler<TEvent>;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventHandleType"></param>
        /// <param name="type"></param>
        void Subscribe(Type eventType, Type eventHandleType, string type = ExchangeType.Topic);
    }

}
