using MassTransit;
using Microsoft.Extensions.Logging;
using StorageService.Events;

namespace StorageService.Handlers
{
    public class EmailReadEventHandler : IConsumer<EmailReadEvent>
    {
        private readonly FileStorage fileStorage;
        private readonly ILogger<EmailReadEventHandler> logger;

        public EmailReadEventHandler(FileStorage fileStorage, ILogger<EmailReadEventHandler> logger)
        {
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
