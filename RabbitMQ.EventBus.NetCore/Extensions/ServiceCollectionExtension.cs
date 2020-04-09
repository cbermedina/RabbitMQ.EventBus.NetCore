using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.EventBus.NetCore;
using RabbitMQ.EventBus.NetCore.Configurations;
using RabbitMQ.EventBus.NetCore.Events;
using RabbitMQ.EventBus.NetCore.Factories;
using RabbitMQ.EventBus.NetCore.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="eventBusOptionAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, string connectionString, Action<RabbitMQEventBusConnectionConfigurationBuild> eventBusOptionAction)
        {
            RabbitMQEventBusConnectionConfiguration configuration = new RabbitMQEventBusConnectionConfiguration();
            RabbitMQEventBusConnectionConfigurationBuild configurationBuild = new RabbitMQEventBusConnectionConfigurationBuild(configuration);
            eventBusOptionAction?.Invoke(configurationBuild);
            services.TryAddSingleton<IRabbitMQPersistentConnection>(options =>
            {
                ILogger<DefaultRabbitMQPersistentConnection> logger = options.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                IConnectionFactory factory = new ConnectionFactory
                {
                    AutomaticRecoveryEnabled = configuration.AutomaticRecoveryEnabled,
                    NetworkRecoveryInterval = configuration.NetworkRecoveryInterval,
                    Uri = new Uri(connectionString),
                };
                var connection = new DefaultRabbitMQPersistentConnection(configuration, factory, logger);
                connection.TryConnect();
                return connection;
            });
            services.TryAddSingleton<IEventHandlerModuleFactory, EventHandlerModuleFactory>();
            services.TryAddSingleton<IRabbitMQEventBus, DefaultRabbitMQEventBus>();
            foreach (Type mType in typeof(IEvent).GetAssemblies())
            {
                services.TryAddTransient(mType);
                foreach (Type hType in typeof(IEventHandler<>).GetMakeGenericType(mType))
                {
                    services.TryAddTransient(hType);
                }
            }
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void RabbitMQEventBusAutoSubscribe(this IApplicationBuilder app)
        {
            IRabbitMQEventBus eventBus = app.ApplicationServices.GetRequiredService<IRabbitMQEventBus>();
            ILogger<IRabbitMQEventBus> logger = app.ApplicationServices.GetRequiredService<ILogger<IRabbitMQEventBus>>();
            using (logger.BeginScope("EventBus Subscribe"))
            {
                foreach (Type mType in typeof(IEvent).GetAssemblies())
                {
                    foreach (Type hType in typeof(IEventHandler<>).GetMakeGenericType(mType))
                    {
                        logger.LogInformation($"{mType.Name}=>{hType.Name}");
                        eventBus.Subscribe(mType, hType);
                    }
                }
                logger.LogInformation($"Ok.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="moduleOptions"></param>
        public static void RabbitMQEventBusModule(this IApplicationBuilder app, Action<RabbitMQEventBusModuleOption> moduleOptions)
        {
            IEventHandlerModuleFactory factory = app.ApplicationServices.GetRequiredService<IEventHandlerModuleFactory>();
            RabbitMQEventBusModuleOption moduleOption = new RabbitMQEventBusModuleOption(factory, app.ApplicationServices);
            moduleOptions?.Invoke(moduleOption);
        }
    }
}

