using System;
using System.Threading.Tasks;
using MassTransit.Audit;

namespace Docker.Core.Messaging
{
    public class MessageAuditStore : IMessageAuditStore
    {
        public async Task StoreMessage<T>(T message, MessageAuditMetadata metadata) where T : class
        {
            Console.WriteLine($"Auditing {metadata.MessageId}");
        }
    }
}
