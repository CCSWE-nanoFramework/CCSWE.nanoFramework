using CCSWE.nanoFramework.WebServer.Authentication;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication
{
    internal class SuccessAuthenticationHandler : AuthenticationHandler
    {
        public SuccessAuthenticationHandler()
        {
            Reset();
        }

        public static bool AuthenticateCalled { get; private set; }

        public override AuthenticateResult Authenticate()
        {
            AuthenticateCalled = true;

            return AuthenticateResult.Success();
        }

        public static void Reset()
        {
            AuthenticateCalled = false;
        }
    }
}
