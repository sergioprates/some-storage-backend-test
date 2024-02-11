using Moq.AutoMock;

namespace StorageServiceTests.Configurations
{
    public abstract class BaseTest
    {
        protected AutoMocker AutoMock = new AutoMocker();
    }
}
