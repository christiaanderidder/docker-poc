using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Docker.Core.Messaging
{
    public class LoggingReceiveMessageObserver : IReceiveObserver
    {
        private readonly ILogger _logger;

        public LoggingReceiveMessageObserver(ILogger logger)
        {
            _logger = logger;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class {
            _logger.LogError(exception, "Consumer {ConsumerType} failed to process the message {@Message} after {Duration}", consumerType, context.Message, duration);
            return Task.CompletedTask;
        }
        
        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            _logger.LogInformation("Consumer {ConsumerType} has sucesfully processed message {@Message} after {Duration}", consumerType, context.Message, duration);
            return Task.CompletedTask;
        }

        public Task PostReceive(ReceiveContext context) => Task.CompletedTask;
        public Task PreReceive(ReceiveContext context) => Task.CompletedTask;
        public Task ReceiveFault(ReceiveContext context, Exception exception) => Task.CompletedTask;
    }
}
