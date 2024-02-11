using FluentAssertions;
using StorageService.Events;

namespace StorageServiceTests.Events
{
    public class EmailReadEventTests
    {
        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var emailReadEvent = new EmailReadEvent
            {
                Referrer = "https://example.com",
                UserAgent = "Test User Agent",
                IPAddress = "192.168.1.1",
                VisitDateTime = new DateTime(2022, 12, 19, 14, 16, 49, 960, DateTimeKind.Utc)
            };

            // Act
            var result = emailReadEvent.ToString();

            // Assert
            result.Should().Be("2022-12-19T14:16:49.9600000Z|https://example.com|Test User Agent|192.168.1.1");
        }

        [Fact]
        public void ToString_WithoutIPAddress_ShouldReturnFormattedStringWithNullIPAddress()
        {
            // Arrange
            var emailReadEvent = new EmailReadEvent
            {
                Referrer = "https://example.com",
                UserAgent = "Test User Agent",
                VisitDateTime = new DateTime(2022, 12, 19, 14, 16, 49, 960, DateTimeKind.Utc)
            };

            // Act
            var result = emailReadEvent.ToString();

            // Assert
            result.Should().Be("2022-12-19T14:16:49.9600000Z|https://example.com|Test User Agent|");
        }
    }
}
