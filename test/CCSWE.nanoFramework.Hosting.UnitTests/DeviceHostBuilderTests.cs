using System;
using CCSWE.nanoFramework.Hosting.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Hosting.UnitTests
{
    [TestClass]
    public class DeviceHostBuilderTests
    {
        [TestMethod]
        public void Build_registers_HostBuilderContext()
        {
            var sut = new DeviceHostBuilder();
            using var host = sut.Build();

            Assert.IsNotNull(host.Services.GetRequiredService(typeof(HostBuilderContext)));
        }

        [TestMethod]
        public void Build_registers_IHost()
        {
            var sut = new DeviceHostBuilder();
            using var host = sut.Build();

            Assert.IsNotNull(host.Services.GetRequiredService(typeof(IHost)));
        }

        [TestMethod]
        public void Build_registers_IServiceProvider()
        {
            var sut = new DeviceHostBuilder();
            using var host = sut.Build();

            Assert.IsNotNull(host.Services.GetRequiredService(typeof(IServiceProvider)));
        }

        [TestMethod]
        public void Build_returns_IHost()
        {
            var sut = new DeviceHostBuilder();
            using var host = sut.Build();

            Assert.IsNotNull(host);
        }

        [TestMethod]
        public void Build_throws_if_called_more_than_once()
        {
            var sut = new DeviceHostBuilder();
            using var host = sut.Build();

            Assert.IsNotNull(host);
            Assert.ThrowsException(typeof(InvalidOperationException), () => sut.Build());
        }

        [TestMethod]
        public void ConfigureServices_can_be_called_multiple_times()
        {
            var callCount = 0; // Verify ordering
            var sut = new DeviceHostBuilder()
                .ConfigureServices((services) =>
                {
                    Assert.AreEqual(0, callCount++);
                    services.AddTransient(typeof(MockServiceA), typeof(MockServiceA));
                })
                .ConfigureServices((services) =>
                {
                    Assert.AreEqual(1, callCount++);
                    services.AddTransient(typeof(MockServiceB), typeof(MockServiceB));
                });

            using var host = sut.Build();

            Assert.AreEqual(2, callCount);

            Assert.IsNotNull(host.Services.GetRequiredService(typeof(MockServiceA)));
            Assert.IsNotNull(host.Services.GetRequiredService(typeof(MockServiceB)));
        }

        [TestMethod]
        public void ConfigureServices_throws_if_configureDelegate_is_null()
        {
            var sut = new DeviceHostBuilder();

            Assert.ThrowsException(typeof(ArgumentNullException), () => sut.ConfigureServices(null!));
        }

        [TestMethod]
        public void Properties_are_available_in_builder_and_context()
        {
            var sut = new DeviceHostBuilder();

            sut.ConfigureServices((context, _) =>
            {
                Assert.AreEqual("value1", context.Properties["key1"]);
                Assert.AreEqual("value2", context.Properties["key2"]);
            });

            sut.Properties["key1"] = "value1";
            sut.Properties["key2"] = "value2";

            using var _ = sut.Build();

            Assert.AreEqual("value1", sut.Properties["key1"]);
            Assert.AreEqual("value2", sut.Properties["key2"]);
        }

        [TestMethod]
        public void UseDefaultServiceProvider_throws_if_configureDelegate_is_null()
        {
            var sut = new DeviceHostBuilder();

            Assert.ThrowsException(typeof(ArgumentNullException), () => sut.UseDefaultServiceProvider(null!));
        }
    }
}
