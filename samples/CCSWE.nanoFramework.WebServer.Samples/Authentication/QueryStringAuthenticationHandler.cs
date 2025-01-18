using CCSWE.nanoFramework.WebServer.Authentication;

namespace CCSWE.nanoFramework.WebServer.Samples.Authentication
{
    internal class QueryStringAuthenticationHandler: AuthenticationHandler
    {
        // TODO: Implement this...
        public override AuthenticateResult Authenticate()
        {
            /*
            var urlParts = Request.RawUrl.Split('?');

            if (urlParts.Length <= 1)
            {
                return AuthenticateResult.NoResult();
            }

            
            return urlParts.Length > 1 && urlParts[1].Contains("authenticated")
                ? AuthenticateResult.Success()
                : AuthenticateResult.Fail("'authenticated' query parameter not supplied");
            */

            return AuthenticateResult.Success();
        }
    }
}
