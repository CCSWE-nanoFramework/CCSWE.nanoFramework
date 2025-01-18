namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication
{
    internal static class AuthenticationHandlerMocks
    {
        public static void Reset()
        {
            ExceptionAuthenticationHandler.Reset();
            FailAuthenticationHandler.Reset();
            NoResultAuthenticationHandler.Reset();
            SuccessAuthenticationHandler.Reset();
        }
    }
}
