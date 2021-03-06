using Docker.Core;
using Docker.Data;
using Docker.Scheduler.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Docker.Scheduler
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
                    services.AddDockerCore(hostContext.Configuration);
                    services.AddDockerData();

                    services.AddAndConfigureMassTransit(hostContext.Configuration, (cfg) =>
                    {

                    });

                    services.AddQuartz(cfg =>
                    {
                        cfg.UseMicrosoftDependencyInjectionJobFactory();
                    });

                    services.AddSingleton<MassTransitRecurringJobPublisher>();
                    services.AddHostedService<TaskScheduler>();
                });
        }
    }
}
