using Docker.Core;
using Docker.Data;
using Docker.Worker.Consumers;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Docker.Worker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var builder = CreateHostBuilder(args);

            if (isService)
            {
                var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                builder = isWindows ? builder.UseWindowsService() : builder.UseSystemd();

                await builder.Build().RunAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, cfg) => AppConfiguration.ConfigureJsonConfig(ctx.HostingEnvironment, cfg))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDockerCore();
                    services.AddDockerData();
                    services.AddAndConfigureMassTransit(hostContext.Configuration, (cfg) =>
                    {
                        cfg.AddConsumer<RecurringJobConsumer>();
                        cfg.AddConsumer<OfferUpdatedEventConsumer>();
                    });
                    
                    services.AddMassTransitHostedService();
                });
        }
    }
}
