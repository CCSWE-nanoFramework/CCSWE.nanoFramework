using CCSWE.nanoFramework.Net;
using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Controllers
{
    [Route("/api/ControllerMock/")]
    internal class ControllerMock : ControllerBase
    {
        public ControllerMock()
        {
            Reset();
        }

        public static string? HttpMethod { get; set; }

        // TODO: Handle typed parameters??
        //public static int IntParameter { get; set; }

        public static string? StringParameter { get; set; }

        [HttpGet("GET/")]
        public void HandleGet()
        {
            HandleRequest(HttpMethods.Get);
            Ok("HandleGet()", MimeType.Text.Plain);
        }

        [HttpGet("/api/ControllerMock/GET/{StringParameter}")]
        public void HandleGet(string stringParameter)
        {
            HandleRequest(HttpMethods.Get, stringParameter);
            Ok("HandleGet(string stringParameter)", MimeType.Text.Plain);
        }

        private static void HandleRequest(string httpMethod, string? stringParameter = null)
        {
            HttpMethod = httpMethod;
            StringParameter = stringParameter;
        }

        private static void Reset()
        {
            HttpMethod = null;
            //IntParameter = 0;
            StringParameter = null;
        }
    }
}
