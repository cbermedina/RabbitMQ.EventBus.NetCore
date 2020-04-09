using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.EventBus.NetCore.Extensions
{
    public static class LoggerExtension
    {

        private static Action<ILogger, string, Exception> _informationAction = null;
        private static Action<ILogger, string, Exception> _warningAction = null;
        private static Action<ILogger, string, Exception> _errorAction = null;

        static LoggerExtension()
        {
            _informationAction = LoggerMessage.Define<string>(LogLevel.Information, new EventId(1, nameof(WriteLog)), "RabbitMQEventBus {LogContent}");
            _warningAction = LoggerMessage.Define<string>(LogLevel.Warning, new EventId(1, nameof(WriteLog)), "RabbitMQEventBus {LogContent}");
            _errorAction = LoggerMessage.Define<string>(LogLevel.Error, new EventId(1, nameof(WriteLog)), "RabbitMQEventBus {LogContent}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="level"></param>
        /// <param name="LogContent"></param>
        public static void WriteLog(this ILogger logger, LogLevel level, string LogContent)
        {
            switch (level)
            {
                case LogLevel.Error:
                    {
                        _errorAction(logger, LogContent, null);
                        break;
                    }
                case LogLevel.Warning:
                    {
                        _warningAction(logger, LogContent, null);
                        break;
                    }
                default:
                    {
                        _informationAction(logger, LogContent, null);
                        break;
                    }
            }
        }

    }
}
