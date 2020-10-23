using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Http.BatchFormatters;

namespace Docker.Core
{
    public static class LoggingConfiguration
    {
        private const string LOGSTASH_URL_SETTING = "Logstash:Url";
        private const string CONSOLE_LOGGING_TEMPLATE = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}";
        private const string BUFFER_NAME_PATTERN = "Docker-POC-Log-Buffer-{Date}.json";

        public static IHostBuilder ConfigureLogging(this IHostBuilder builder)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                var logstashUrl = hostingContext.Configuration.GetValue<string>(LOGSTASH_URL_SETTING)
                                   ?? throw new InvalidOperationException("Logstash url missing in configuration");
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                .WriteTo.Async(a => a.Console(outputTemplate: CONSOLE_LOGGING_TEMPLATE))
                .WriteTo.DurableHttpUsingTimeRolledBuffers(
                        logstashUrl,
                        BUFFER_NAME_PATTERN,
                        batchFormatter: new ArrayBatchFormatter(),
                        textFormatter: new JsonFormatter());
            });
            return builder;
        }

        public static ILogger CreateEmergencyLogger()
            => new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
    }
}
