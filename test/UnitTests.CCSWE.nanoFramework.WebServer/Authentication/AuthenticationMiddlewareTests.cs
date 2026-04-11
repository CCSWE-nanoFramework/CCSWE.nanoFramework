using CCSWE.nanoFramework.WebServer.Authentication;
using nanoFramework.TestFramework;
using UnitTests.CCSWE.nanoFramework.WebServer.Mocks;
using UnitTests.CCSWE.nanoFramework.WebServer.Mocks.Authentication;
using UnitTests.CCSWE.nanoFramework.WebServer.Mocks.Http;

namespace UnitTests.CCSWE.nanoFramework.WebServer.Authentication;
[TestClass]
public class AuthenticationMiddlewareTests
{
    [TestMethod]
    public void Invoke_authentication_failure()
    {
        var authenticationService = new AuthenticationServiceMock();
        var context = new HttpContextMock { ShouldFail = true };
        var next = new RequestDelegateMock();

        var sut = new AuthenticationMiddleware(authenticationService);

        sut.Invoke(context, next.Invoke);

        Assert.IsNotNull(context.AuthenticateResult);

        Assert.IsNotNull(context.AuthenticateResult.Failure);
        Assert.IsFalse(context.AuthenticateResult.None);
        Assert.IsFalse(context.AuthenticateResult.Succeeded);

        Assert.IsTrue(next.Invoked);
    }

    [TestMethod]
    public void Invoke_authentication_no_result()
    {
        var context = new HttpContextMock();
        var authenticationService = new AuthenticationServiceMock();
        var next = new RequestDelegateMock();

        var sut = new AuthenticationMiddleware(authenticationService);

        sut.Invoke(context, next.Invoke);

        Assert.IsNotNull(context.AuthenticateResult);

        Assert.IsNull(context.AuthenticateResult.Failure);
        Assert.IsTrue(context.AuthenticateResult.None);
        Assert.IsFalse(context.AuthenticateResult.Succeeded);

        Assert.IsTrue(next.Invoked);
    }

    [TestMethod]
    public void Invoke_authentication_success()
    {
        var authenticationService = new AuthenticationServiceMock();
        var context = new HttpContextMock { ShouldSucceed = true };
        var next = new RequestDelegateMock();

        var sut = new AuthenticationMiddleware(authenticationService);

        sut.Invoke(context, next.Invoke);

        Assert.IsNotNull(context.AuthenticateResult);

        Assert.IsNull(context.AuthenticateResult.Failure);
        Assert.IsFalse(context.AuthenticateResult.None);
        Assert.IsTrue(context.AuthenticateResult.Succeeded);
    
        Assert.IsTrue(next.Invoked);
    }
}


