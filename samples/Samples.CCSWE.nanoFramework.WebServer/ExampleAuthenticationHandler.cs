using CCSWE.nanoFramework.WebServer.Authentication;

namespace Samples.CCSWE.nanoFramework.WebServer;

internal class ExampleAuthenticationHandler : AuthenticationHandler
{
    public override AuthenticateResult Authenticate()
    {
        // Check if the 'authenticated' query parameter is present
        // Please don't implement your authentication like this in a real-world scenario
        return !string.IsNullOrEmpty(Request.QueryString) && Request.QueryString.Contains("authenticated")
            ? AuthenticateResult.Success()
            : AuthenticateResult.Fail("'authenticated' query parameter not supplied");
    }
}
