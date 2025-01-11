using Microsoft.Extensions.DependencyInjection;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.FileStorage.UnitTests
{
    [TestClass]
    public class BootstrapperTests
    {
        [TestMethod]
        public void AddFileStorage_should_register_FileStorage()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddFileStorage();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var result1 = serviceProvider.GetService(typeof(IFileStorage));
            var result2 = serviceProvider.GetService(typeof(IFileStorage));

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(FileStorage));
            Assert.AreEqual(result1, result2);
        }
    }
}
