using System;
using Docker.Core.Configuration;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docker.Core
{
    public static class DockerCoreServiceCollectionExtensions
    {
        public static void AddDockerCore(this IServiceCollection services)
        {
            
        }

        public static void AddAndConfigureMassTransit(this IServiceCollection services, IConfiguration config, Action<IServiceCollectionBusConfigurator> configure = null)
        {
            services.AddMassTransit(cfg =>
            {
                var rabbitMqConfig = config.GetSection("RabbitMq").Get<RabbitMqConfiguration>();
                cfg.SetKebabCaseEndpointNameFormatter();

                cfg.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqConfig.Host);

                    // Automatically configure endpoints for all consumers
                    cfg.ConfigureEndpoints(context);
                });

                // Allows the caller to set publishers and consumers
                if(configure != null) configure(cfg);
            });

            services.AddMassTransitHostedService();
        }
    }
}
