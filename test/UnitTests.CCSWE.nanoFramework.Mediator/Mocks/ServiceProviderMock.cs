using System;

namespace UnitTests.CCSWE.nanoFramework.Mediator.Mocks;
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
