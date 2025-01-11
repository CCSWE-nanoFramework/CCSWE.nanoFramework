using CCSWE.nanoFramework.Hosting.UnitTests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.Hosting;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Hosting.UnitTests
{
    [TestClass]
    public class ServiceProviderExtensionsTests
    {

        [TestMethod]
        public void GetHostedServices_handles_zero_registered_IHostedServices()
        {
            var sut = new ServiceCollection().BuildServiceProvider();
            var actual = sut.GetHostedServices();

            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void GetHostedServices_returns_registered_IHostedServices()
        {
            var sut = new ServiceCollection()
                .AddTransient(typeof(MockServiceA))
                .AddTransient(typeof(MockServiceB))
                .AddHostedService(typeof(MockHostedServiceA))
                .AddHostedService(typeof(MockHostedServiceB))
                .BuildServiceProvider();

            var actual = sut.GetHostedServices();

            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Length);
            Assert.IsInstanceOfType(actual[0], typeof(MockHostedServiceA));
            Assert.IsInstanceOfType(actual[1], typeof(MockHostedServiceB));
        }
    }
}
