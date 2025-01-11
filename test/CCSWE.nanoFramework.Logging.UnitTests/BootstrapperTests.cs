using nanoFramework.TestFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Logging.UnitTests
{
    [TestClass]
    public class BootstrapperTests
    {
        [TestMethod]
        public void AddLogging_adds_DebugLogger()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var actual = serviceProvider.GetServices(typeof(ILogger));

            Assert.IsNotNull(actual[0]);
            Assert.AreEqual(1, actual.Length);
            Assert.IsInstanceOfType(actual[0], typeof(DebugLogger));
        }

        [TestMethod]
        public void AddLogging_adds_LoggerOptions()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(options => options.MinLogLevel = LogLevel.Information);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var actual = serviceProvider.GetServices(typeof(LoggerOptions));

            Assert.IsNotNull(actual[0]);
            Assert.AreEqual(1, actual.Length);
            Assert.IsInstanceOfType(actual[0], typeof(LoggerOptions));
            Assert.AreEqual(LogLevel.Information, ((LoggerOptions)actual[0]).MinLogLevel);
        }

        [TestMethod]
        public void AddLogging_is_idempotent()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(options => options.MinLogLevel = LogLevel.Information);
            serviceCollection.AddLogging(options => options.MinLogLevel = LogLevel.Trace);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var actualLogger = serviceProvider.GetServices(typeof(ILogger));

            Assert.IsNotNull(actualLogger[0]);
            Assert.AreEqual(1, actualLogger.Length);
            Assert.IsInstanceOfType(actualLogger[0], typeof(DebugLogger));

            var actualOptions = serviceProvider.GetServices(typeof(LoggerOptions));

            Assert.IsNotNull(actualOptions[0]);
            Assert.AreEqual(1, actualOptions.Length);
            Assert.IsInstanceOfType(actualOptions[0], typeof(LoggerOptions));
            Assert.AreEqual(LogLevel.Information, ((LoggerOptions)actualOptions[0]).MinLogLevel);
        }
    }
}

