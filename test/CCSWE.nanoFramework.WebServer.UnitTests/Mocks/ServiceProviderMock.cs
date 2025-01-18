using System;
using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks
{
    internal class ServiceProviderMock: IServiceProvider
    {
        private readonly ServiceProvider _serviceProvider = new ServiceCollection().BuildServiceProvider();

        public object GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

        public object[] GetService(Type[] serviceType) => _serviceProvider.GetService(serviceType);
    }
}
