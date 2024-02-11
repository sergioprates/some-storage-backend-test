using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StorageService.Handlers;

namespace StorageService
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly EmailReadEventHandler emailReadEventHandler;
        private readonly ILogger<ConsumerBackgroundService> logger;

        public ConsumerBackgroundService(
            EmailReadEventHandler emailReadEventHandler,
            ILogger<ConsumerBackgroundService> logger)
        {
            this.emailReadEventHandler = emailReadEventHandler;
            this.logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Service started.");
            await Task.Delay(Timeout.Infinite);
            this.logger.LogInformation("Service shutdown");
        }
    }
}
