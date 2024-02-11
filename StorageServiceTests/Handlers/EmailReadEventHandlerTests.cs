using MassTransit;
using Microsoft.Extensions.Configuration;
using Moq;
using StorageService;
using StorageService.Events;
using StorageService.Handlers;
using StorageServiceTests.Configurations;

namespace StorageServiceTests.Handlers
{
    public class EmailReadEventHandlerTests : BaseTest
    {
        [Fact]
        public async Task Consume_ShouldSaveToStorage()
        {
            // Arrange
            var emailReadEvent = new EmailReadEvent();

            AutoMock.GetMock<IConfiguration>().Setup(c => c["FilePath"]).Returns("");
            AutoMock.GetMock<ConsumeContext<EmailReadEvent>>().Setup(c => c.Message).Returns(emailReadEvent);
            AutoMock.GetMock<ConsumeContext<EmailReadEvent>>().Setup(c => c.CancellationToken).Returns(CancellationToken.None);

            // Act
            await AutoMock.CreateInstance<EmailReadEventHandler>().Consume(AutoMock.Get<ConsumeContext<EmailReadEvent>>());

            // Assert
            AutoMock.GetMock<FileStorage>().Verify(
                x => x.SaveAsync(emailReadEvent, CancellationToken.None),
                Times.Once
            );
        }
    }
}
