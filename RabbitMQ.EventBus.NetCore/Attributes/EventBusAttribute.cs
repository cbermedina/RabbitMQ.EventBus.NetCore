using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.EventBus.NetCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EventBusAttribute: Attribute
    {

        /// <summary>
        /// 
        /// </summary>
        public string Queue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Exchange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RoutingKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EventBusAttribute()
        {
            RoutingKey = "";
        }


    }
}
