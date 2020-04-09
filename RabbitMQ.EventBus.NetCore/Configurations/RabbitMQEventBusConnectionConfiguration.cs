using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.EventBus.NetCore.Configurations
{
    public sealed class RabbitMQEventBusConnectionConfiguration
    {

        /// <summary>
        /// 
        /// </summary>
        public string ClientProvidedName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FailReConnectRetryCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AutomaticRecoveryEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan NetworkRecoveryInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan ConsumerFailRetryInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LogLevel Level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RabbitMQEventBusConnectionConfiguration()
        {
            Level = LogLevel.Information;
            FailReConnectRetryCount = 50;
            NetworkRecoveryInterval = TimeSpan.FromSeconds(5);
            AutomaticRecoveryEnabled = true;
            ConsumerFailRetryInterval = TimeSpan.FromSeconds(1);
        }

    }
}
