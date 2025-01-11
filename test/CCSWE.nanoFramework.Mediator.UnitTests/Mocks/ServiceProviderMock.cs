using System;

namespace CCSWE.nanoFramework.Mediator.UnitTests.Mocks
{
    internal class ServiceProviderMock: IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public object[] GetService(Type[] serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
