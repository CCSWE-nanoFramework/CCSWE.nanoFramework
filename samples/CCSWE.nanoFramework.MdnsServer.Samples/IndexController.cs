using CCSWE.nanoFramework.WebServer;
using CCSWE.nanoFramework.WebServer.Authorization;

namespace CCSWE.nanoFramework.MdnsServer.Samples
{
    // A controller groups related HTTP endpoint handlers under a common route prefix.
    // The [Route] attribute below sets the base path for all methods in this class.
    // Using "/" means this controller responds to requests at the root of the server.
    [Route("/")]
    public class IndexController : ControllerBase
    {
        // [AllowAnonymous] disables authentication checks for this endpoint.
        // Without it, the web server would require an Authorization header by default.
        //
        // [HttpGet] maps this method to HTTP GET requests at the route defined on the class.
        // A browser visiting http://<device-ip>/ or http://mdns-sample.local/ will trigger this.
        [AllowAnonymous]
        [HttpGet]
        public void HandleGet()
        {
            // Ok() sends an HTTP 200 response. The string is JSON-serialized, so the
            // client receives: "Hello World" (with quotes, as a JSON string value).
            Ok("Hello World");
        }
    }
}
