using System;
using CCSWE.nanoFramework.WebServer.Authentication;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication
{
    internal class ExceptionAuthenticationHandler : AuthenticationHandler
    {
        public ExceptionAuthenticationHandler()
        {
            Reset();
        }

        public static bool AuthenticateCalled { get; private set; }

        public override AuthenticateResult Authenticate()
        {
            AuthenticateCalled = true;

            throw new Exception();
        }

        public static void Reset()
        {
            AuthenticateCalled = false;
        }
    }
}
