using System.Threading.Tasks;
using Docker.Core.Jobs;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Docker.Worker.Consumers
{
    public class RecurringJobConsumer : IConsumer<RecurringJob>
    {
        ILogger<RecurringJobConsumer> _logger;

        public RecurringJobConsumer(ILogger<RecurringJobConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RecurringJob> context)
        {
            _logger.LogInformation($"Recurring job received: {context.Message.Message}");
        }
    }
}
