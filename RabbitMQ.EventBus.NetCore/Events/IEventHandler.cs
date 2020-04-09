using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.EventBus.NetCore.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task Handle(TEvent message);
    }
    /// <summary>
    /// 
    /// </summary>
    public class MessageEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public string Original { get; }
        /// <summary>
        /// 
        /// </summary>
        public bool Redelivered { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="redelivered"></param>
        public MessageEventArgs(string original, bool redelivered)
        {
            Original = original ?? throw new ArgumentNullException(nameof(original));
            Redelivered = redelivered;
        }
    }
}
