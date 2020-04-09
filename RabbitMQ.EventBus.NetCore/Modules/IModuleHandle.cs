using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.EventBus.NetCore.Modules
{
    public interface IModuleHandle
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task PublishEvent(EventBusArgs e);
        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task SubscribeEvent(EventBusArgs e);
    }
}
