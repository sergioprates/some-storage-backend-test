using MassTransit;
using StorageService.Events;

namespace PixelApi.Storages
{
    public class VisitorsService
    {
        private readonly IPublishEndpoint rabbitMqService;

        public VisitorsService(IPublishEndpoint rabbitMqService)
        {
            this.rabbitMqService = rabbitMqService;
        }

        public async Task SaveVisitorInfoAsync(EmailReadEvent trackingInfo, CancellationToken cancellationToken) =>
            await this.rabbitMqService.Publish(trackingInfo, cancellationToken).ConfigureAwait(false);
    }
}
