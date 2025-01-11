using System;
using CCSWE.nanoFramework.Hosting.UnitTests.Mocks;
using CCSWE.nanoFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using nanoFramework.Hosting;
using nanoFramework.TestFramework;

// ReSharper disable ObjectCreationAsStatement
namespace CCSWE.nanoFramework.Hosting.UnitTests.Internal
{
    [TestClass]
    public class DeviceHostTests
    {
        [TestMethod]
        public void ctor_throws_if_loggerFactory_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new CCSWE.nanoFramework.Hosting.Internal.DeviceHost(new ServiceCollection().BuildServiceProvider(), null!));
        }

        [TestMethod]
        public void ctor_throws_if_services_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new CCSWE.nanoFramework.Hosting.Internal.DeviceHost(null!, new DebugLogger(new LoggerOptions(LogLevel.Debug))));
        }

        [TestMethod]
        public void Start_starts_IHostedService()
        {
            var service = new MockHostedService(startThrows: false, stopThrows: false);
            var sut = new DeviceHostBuilder()
                .ConfigureServices(services => services.AddSingleton(typeof(IHostedService), service))
                .Build();

            var registeredService = sut.Services.GetRequiredService(typeof(IHostedService)) as MockHostedService;

            sut.Start();

            Assert.IsNotNull(registeredService);
            // TODO: Bug someone to publish the test framework changes :P
            Assert.IsTrue(registeredService.IsStarted);
            Assert.IsFalse(registeredService.IsStopped);
        }

        [TestMethod]
        public void Start_throws_if_IHostedService_throws()
        {
            var service = new MockHostedService(startThrows: true, stopThrows: false);
            var sut = new DeviceHostBuilder()
                .ConfigureServices(services => services.AddSingleton(typeof(IHostedService), service))
                .Build();

            Assert.ThrowsException(typeof(AggregateException), () => sut.Start());
        }

        [TestMethod]
        public void Stop_stops_IHostedService()
        {
            var service = new MockHostedService(startThrows: false, stopThrows: false);
            var sut = new DeviceHostBuilder()
                .ConfigureServices(services => services.AddSingleton(typeof(IHostedService), service))
                .Build();

            sut.Start();
            sut.Stop();

            var registeredService = sut.Services.GetRequiredService(typeof(IHostedService)) as MockHostedService;

            Assert.IsNotNull(registeredService);
            Assert.IsTrue(registeredService.IsStarted);
            Assert.IsTrue(registeredService.IsStopped);
        }

        [TestMethod]
        public void Stop_throws_if_IHostedService_throws()
        {
            var service = new MockHostedService(startThrows: false, stopThrows: true);
            var sut = new DeviceHostBuilder()
                .ConfigureServices(services => services.AddSingleton(typeof(IHostedService), service))
                .Build();

            sut.Start();

            Assert.ThrowsException(typeof(AggregateException), () => sut.Stop());
        }
    }
}
