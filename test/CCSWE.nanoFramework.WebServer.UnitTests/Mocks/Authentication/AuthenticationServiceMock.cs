using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication
{
    internal class AuthenticationServiceMock : IAuthenticationService
    {
        public AuthenticateResult Authenticate(HttpContext context)
        {
            return context switch
            {
                HttpContextMock { ShouldSucceed: true } => AuthenticateResult.Success(),
                HttpContextMock { ShouldFail: true } => AuthenticateResult.Fail("Mock failure"),
                _ => AuthenticateResult.NoResult()
            };
        }
    }
}