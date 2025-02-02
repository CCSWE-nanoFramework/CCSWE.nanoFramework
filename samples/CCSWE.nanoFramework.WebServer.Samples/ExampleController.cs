using System.Net;
using CCSWE.nanoFramework.IO;
using CCSWE.nanoFramework.WebServer.Authorization;
using CCSWE.nanoFramework.WebServer.Samples.Services;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.WebServer.Samples
{
    [Route("/example")]
    public class ExampleController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger _logger;

        // Dependency injection is supported for controllers
        public ExampleController(IDataService dataService, ILogger logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        // By default, all methods require authorization
        // Use the [AllowAnonymous] attribute to allow unauthenticated access
        // This attribute can be applied to the class or method level
        [AllowAnonymous]
        [HttpGet]
        public void HandleGet()
        {
            _logger.LogInformation("=> HandleGet");

            var data = _dataService.GetData();

            if (data is null)
            {
                // ControllerBase provides helper methods for common HTTP status codes
                NotFound("Data: <null>");
                return;
            }

            // By default, ControllerBase will serialize the object as JSON
            Ok(data);
        }

        // Partial routes (eg, those that do not start with a '/') are appended to the route defined on the class
        [HttpGet("{value}")]
        public void HandleGetWithParameter(string value)
        {
            _logger.LogInformation("=> HandleGetWithParameter");
            _logger.LogInformation($"Value: {value}");

            _dataService.SetValue(value);

            StatusCode(HttpStatusCode.Created);
        }

        // Complete routes with a leading '/' will override the route defined on the class
        [HttpPost("/example2")]
        public void HandlePost()
        {
            var value = Request.Body.ReadAsString();

            _logger.LogInformation("=> HandlePost");
            _logger.LogInformation($"Value: {value}");

            _dataService.SetValue(value);

            StatusCode(HttpStatusCode.Created);
        }
    }
}
