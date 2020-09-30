using System.Threading.Tasks;
using Docker.Core.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Docker.Worker.Consumers
{
    public class OfferUpdatedEventConsumer : IConsumer<OfferUpdatedEvent>
    {
        ILogger<OfferUpdatedEventConsumer> _logger;

        public OfferUpdatedEventConsumer(ILogger<OfferUpdatedEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OfferUpdatedEvent> context)
        {
            _logger.LogInformation("Price: {Price}", context.Message.Price);
        }
    }
}
