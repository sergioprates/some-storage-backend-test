using Moq.AutoMock;

namespace PixelApiTests.Configurations
{
    public abstract class BaseTest
    {
        protected AutoMocker AutoMock = new AutoMocker();
    }
}
