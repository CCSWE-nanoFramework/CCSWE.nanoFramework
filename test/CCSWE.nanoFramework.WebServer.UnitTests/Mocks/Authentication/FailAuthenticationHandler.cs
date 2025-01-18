using CCSWE.nanoFramework.WebServer.Authentication;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication
{
    internal class FailAuthenticationHandler : AuthenticationHandler
    {
        public FailAuthenticationHandler()
        {
            Reset();
        }

        public static bool AuthenticateCalled { get; private set; }

        public override AuthenticateResult Authenticate()
        {
            AuthenticateCalled = true;

            return AuthenticateResult.Fail(nameof(FailAuthenticationHandler));
        }

        public static void Reset()
        {
            AuthenticateCalled = false;
        }
    }
}
