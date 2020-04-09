using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.EventBus.NetCore.Modules
{
    public class EventBusArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Exchange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Queue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RoutingKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ExchangeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientProvidedName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="exchangeType"></param>
        /// <param name="clientProvidedName"></param>
        /// <param name="message"></param>
        /// <param name="success"></param>
        public EventBusArgs(string endPoint, string exchange, string queue, string routingKey, string exchangeType, string clientProvidedName, string message, bool success)
        {
            Endpoint = endPoint;
            Exchange = exchange;
            Queue = queue;
            RoutingKey = routingKey;
            ExchangeType = exchangeType;
            ClientProvidedName = clientProvidedName;
            Message = message;
            Success = success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Endpoint}\t{ClientProvidedName}\t{Exchange}\t{ExchangeType}\t{Queue}\t{RoutingKey}\t{Message}";
        }
    }

}
