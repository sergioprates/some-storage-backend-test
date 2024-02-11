using MassTransit;
using Moq;
using PixelApi.Storages;
using PixelApiTests.Configurations;
using StorageService.Events;

namespace PixelApiTests.Visitors
{
    public class VisitorServiceTests : BaseTest
    {
        [Fact]
        public async Task SaveVisitorInfoAsync_ShouldPublishMessage()
        {
            var emailReadEvent = new EmailReadEvent();
            // Act
            await AutoMock.CreateInstance<VisitorsService>().SaveVisitorInfoAsync(emailReadEvent, default);

            // Assert
            AutoMock.GetMock<IPublishEndpoint>().Verify(x => x.Publish(emailReadEvent, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}