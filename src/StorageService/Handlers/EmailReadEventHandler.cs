using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StorageService.Events;

namespace StorageService.Handlers
{
    public class EmailReadEventHandler : IConsumer<EmailReadEvent>
    {
        private readonly IBus bus;
        private readonly string queueName;
        private readonly FileStorage fileStorage;
        private readonly ILogger<EmailReadEventHandler> logger;

        public EmailReadEventHandler(IBus bus, IConfiguration configuration, FileStorage fileStorage, ILogger<EmailReadEventHandler> logger)
        {
            this.bus = bus;
            this.queueName = configuration["QueueName"]!;
            this.fileStorage = fileStorage;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<EmailReadEvent> context)
        {
            this.logger.LogInformation("Message received.");
            await this.fileStorage.SaveAsync(context.Message, context.CancellationToken);
        }
    }
}
