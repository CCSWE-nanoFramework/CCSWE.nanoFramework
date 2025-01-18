using CCSWE.nanoFramework.WebServer.Authentication;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication
{
    internal class NoResultAuthenticationHandler: AuthenticationHandler
    {
        public NoResultAuthenticationHandler()
        {
            Reset();
        }

        public static bool AuthenticateCalled { get; private set; }

        public override AuthenticateResult Authenticate()
        {
            AuthenticateCalled = true;

            return AuthenticateResult.NoResult();
        }

        public static void Reset()
        {
            AuthenticateCalled = false;
        }
    }
}
