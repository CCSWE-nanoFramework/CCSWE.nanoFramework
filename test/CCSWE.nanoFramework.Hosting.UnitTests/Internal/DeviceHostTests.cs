using System;
using CCSWE.nanoFramework.Hosting.UnitTests.Mocks;
using CCSWE.nanoFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nanoFramework.TestFramework;

// ReSharper disable ObjectCreationAsStatement
namespace CCSWE.nanoFramework.Hosting.UnitTests.Internal
{
    [TestClass]
    public class DeviceHostTests
    {
        [TestMethod]
        public void ctor_throws_if_logger_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new Hosting.Internal.DeviceHost(new ServiceCollection().BuildServiceProvider(), null!));
        }

        [TestMethod]
        public void ctor_throws_if_services_is_null()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            Assert.ThrowsException(typeof(ArgumentNullException), () => new Hosting.Internal.DeviceHost(null!, new ConsoleLogger()));
        }

        [TestMethod]
        public void StartAsync_starts_IHostedService()
        {
            var service = new MockHostedService(startThrows: false, stopThrows: false);
            var sut = new DeviceHostBuilder()
                .ConfigureServices(services => services.AddSingleton(typeof(IHostedService), service))
                .Build();

            var registeredService = sut.Services.GetRequiredService(typeof(IHostedService)) as MockHostedService;

            sut.StartAsync();

            Assert.IsNotNull(registeredService);
            Assert.IsTrue(registeredService.IsStarted);
            Assert.IsFalse(registeredService.IsStopped);
        }

        [TestMethod]
        public void StartAsync_throws_if_IHostedService_throws()
        {
            var service = new MockHostedService(startThrows: true, stopThrows: false);
            var sut = new DeviceHostBuilder()
                .ConfigureServices(services => services.AddSingleton(typeof(IHostedService), service))
                .Build();

            Assert.ThrowsException(typeof(AggregateException), () => sut.StartAsync());
        }

        [TestMethod]
        public void StopAsync_stops_IHostedService()
        {
            var service = new MockHostedService(startThrows: false, stopThrows: false);
            var sut = new DeviceHostBuilder()
                .ConfigureServices(services => services.AddSingleton(typeof(IHostedService), service))
                .Build();

            sut.StartAsync();
            sut.StopAsync();

            var registeredService = sut.Services.GetRequiredService(typeof(IHostedService)) as MockHostedService;

            Assert.IsNotNull(registeredService);
            Assert.IsTrue(registeredService.IsStarted);
            Assert.IsTrue(registeredService.IsStopped);
        }

        [TestMethod]
        public void StopAsync_throws_if_IHostedService_throws()
        {
            var service = new MockHostedService(startThrows: false, stopThrows: true);
            var sut = new DeviceHostBuilder()
                .ConfigureServices(services => services.AddSingleton(typeof(IHostedService), service))
                .Build();

            sut.StartAsync();

            Assert.ThrowsException(typeof(AggregateException), () => sut.StopAsync());
        }
    }
}
