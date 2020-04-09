using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.EventBus.NetCore.Modules
{
    public sealed class RabbitMQEventBusModuleOption
    {
        private readonly IEventHandlerModuleFactory handlerFactory;
        public IServiceProvider ApplicationServices;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlerFactory"></param>
        public RabbitMQEventBusModuleOption(IEventHandlerModuleFactory handlerFactory, IServiceProvider applicationServices)
        {
            this.handlerFactory = handlerFactory ?? throw new ArgumentNullException(nameof(handlerFactory));
            ApplicationServices = applicationServices;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        public void AddModule(IModuleHandle module)
        {
            handlerFactory.TryAddMoudle(module);
        }
    }

}
