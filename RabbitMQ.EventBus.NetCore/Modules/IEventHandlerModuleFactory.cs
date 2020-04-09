using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.EventBus.NetCore.Modules
{
    public interface IEventHandlerModuleFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        void TryAddMoudle(IModuleHandle module);
        /// <summary>
        /// 
        /// </summary>
        void PubliushEvent(EventBusArgs e);
        /// <summary>
        /// 
        /// </summary>
        void SubscribeEvent(EventBusArgs e);

    }
}
