using Docker.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Docker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, cfg) => AppConfiguration.ConfigureJsonConfig(ctx.HostingEnvironment, cfg))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
