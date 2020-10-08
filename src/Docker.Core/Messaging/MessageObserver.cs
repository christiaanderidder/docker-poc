using System;
using System.Threading.Tasks;
using MassTransit;

namespace Docker.Core.Messaging
{
    public class MessageObserver : IReceiveObserver
    {
        public async Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            Console.WriteLine(exception);
        }

        public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            Console.WriteLine(consumerType);
        }

        public async Task PostReceive(ReceiveContext context)
        {
            Console.WriteLine(context.IsDelivered);
        }

        public async Task PreReceive(ReceiveContext context)
        {
            Console.WriteLine(context.IsDelivered);
        }

        public async Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
