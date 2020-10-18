﻿using System;
using Docker.Core.Configuration;
using Docker.Core.Messaging;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Docker.Core
{
    public static class DockerCoreServiceCollectionExtensions
    {
        public static void AddDockerCore(this IServiceCollection services)
        {

        }

        public static void AddAndConfigureMassTransit(this IServiceCollection services, IConfiguration config, Action<IServiceCollectionBusConfigurator>? configure = null)
        {
            var rabbitMqConfig = config.GetSection("RabbitMq").Get<RabbitMqConfiguration>();

            if (rabbitMqConfig == null || rabbitMqConfig.SchedulerEndpoint == null) return;

            services.AddMassTransit(cfg =>
            {
                var schedulerEndpoint = new Uri(rabbitMqConfig.SchedulerEndpoint);

                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.AddMessageScheduler(schedulerEndpoint);

                cfg.UsingRabbitMq((context, cfg) =>
                {

                    cfg.Host(rabbitMqConfig.Host);

                    cfg.UseMessageScheduler(schedulerEndpoint);
                    var loggingService = context.GetService<ILoggerFactory>().CreateLogger<LoggingReceiveMessageObserver>();
                    var loggingObserver = new LoggingReceiveMessageObserver(loggingService);
                    var auditStore = new MessageAuditStore();
                    var receiveObserver = new MessageObserver();

                    cfg.ConnectReceiveObserver(loggingObserver);
                    //cfg.ConnectSendAuditObservers(auditStore);
                    //cfg.ConnectConsumeAuditObserver(auditStore);
                    //cfg.ConnectReceiveObserver(receiveObserver);

                    // Automatically configure endpoints for all consumers
                    cfg.ConfigureEndpoints(context);
                });

                // Allows the caller to set publishers and consumers
                if(configure != null) configure(cfg);
            });
        }
    }
}
