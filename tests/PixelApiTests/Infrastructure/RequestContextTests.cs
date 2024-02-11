using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using PixelApi.Infrastructure;
using PixelApiTests.Configurations;

namespace PixelApiTests.Infrastructure
{
    public class RequestContextTests : BaseTest
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var mockHttpContextAccessor = AutoMock.GetMock<IHttpContextAccessor>();
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Headers["Referer"] = "https://example.com";
            mockHttpContext.Request.Headers["User-Agent"] = "Test User Agent";
            mockHttpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.1");

            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

            // Act
            var requestContext = new RequestContext(mockHttpContextAccessor.Object);

            // Assert
            requestContext.Should().BeEquivalentTo(new
            {
                Referer = "https://example.com",
                UserAgent = "Test User Agent",
                IpAddress = "192.168.1.1"
            });
        }

        [Fact]
        public void Constructor_WithoutHttpContext_ShouldInitializePropertiesWithEmptyValues()
        {
            // Arrange
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(null as HttpContext);

            // Act
            var requestContext = new RequestContext(mockHttpContextAccessor.Object);

            // Assert
            requestContext.Should().BeEquivalentTo(new
            {
                Referer = string.Empty,
                UserAgent = string.Empty,
                IpAddress = string.Empty
            });
        }
    }
}
