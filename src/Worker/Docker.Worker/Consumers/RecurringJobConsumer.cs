using System;
using System.Linq;
using System.Threading.Tasks;
using Docker.Core.Jobs;
using Docker.Data.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Docker.Worker.Consumers
{
    public class RecurringJobConsumer : IConsumer<RecurringJob>
    {
        ILogger<RecurringJobConsumer> _logger;
        private readonly IProductRepository _productRepository;

        public RecurringJobConsumer(ILogger<RecurringJobConsumer> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<RecurringJob> context)
        {
            _logger.LogInformation($"Recurring job received: {context.Message.Message}");

            if (DateTime.Now.Ticks % 5 == 0)
            {
                throw new EverythingIsOnFireException("🔥🔥🔥 Something terrible has happened 🔥🔥🔥");
            }
            switch (context.Message.Type)
            {
                case RecurringJobType.ImportOrders:
                    ImportOrders();
                    break;
                case RecurringJobType.ImportStock:
                    ImportStock();
                    break;
            }
        }

        private void ImportOrders()
        {
            var product = _productRepository.GetAll().First();
            product.Stock -= 1;
            _productRepository.Persist(product);
        }

        private void ImportStock()
        {
            var product = _productRepository.GetAll().First();
            product.Stock += 3;
            _productRepository.Persist(product);
        }
    }

    public class EverythingIsOnFireException: Exception
    {
        public EverythingIsOnFireException(string message): base(message)
        {

        }

        public EverythingIsOnFireException()
        {
        }

        public EverythingIsOnFireException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
