using CCSWE.nanoFramework.Configuration.Internal;
using CCSWE.nanoFramework.Configuration.UnitTests.Mocks;
using CCSWE.nanoFramework.Threading.TestFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Configuration.UnitTests
{
    [TestClass]
    public class BootstrapperTests
    {
        [TestMethod]
        public void AddConfigurationManager_should_register_ConfigurationManager()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                // Arrange
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddSingleton(typeof(ILogger), typeof(LoggerMock));

                // Act
                serviceCollection.AddConfigurationManager();

                var serviceProvider = serviceCollection.BuildServiceProvider();
                var result1 = serviceProvider.GetService(typeof(IConfigurationManager));
                var result2 = serviceProvider.GetService(typeof(IConfigurationManager));

                // Assert
                Assert.IsNotNull(result1);
                Assert.IsInstanceOfType(result1, typeof(ConfigurationManager));
                Assert.AreEqual(result1, result2);

                var configurationManager = (ConfigurationManager)result1;
                configurationManager.Dispose();
            });
        }

        [TestMethod]
        public void AddConfigurationManager_should_register_InternalConfigurationStorage()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(typeof(ILogger), typeof(LoggerMock));

            // Act
            serviceCollection.AddConfigurationManager(options => { options.UseInternalStorage = true;});

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var result1 = serviceProvider.GetService(typeof(IConfigurationStorage));
            var result2 = serviceProvider.GetService(typeof(IConfigurationStorage));

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(InternalConfigurationStorage));
            Assert.AreEqual(result1, result2);
        }

        [TestMethod]
        public void BindConfiguration_adds_ConfigurationDescriptor()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.BindConfiguration(ConfigurationMock.Section, ConfigurationMock.Default);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var result1 = serviceProvider.GetServices(typeof(ConfigurationDescriptor));
            var result2 = serviceProvider.GetServices(typeof(ConfigurationDescriptor));

            // Assert
            Assert.IsTrue(result1.Length > 0);
            Assert.AreEqual(result1.Length, result2.Length);

            var configurationDescriptor = (ConfigurationDescriptor)result1[0];

            Assert.AreEqual(ConfigurationMock.Default, configurationDescriptor.Defaults);
            Assert.AreEqual(ConfigurationMock.Section.ToLower(), configurationDescriptor.Section.ToLower());
        }
    }
}
