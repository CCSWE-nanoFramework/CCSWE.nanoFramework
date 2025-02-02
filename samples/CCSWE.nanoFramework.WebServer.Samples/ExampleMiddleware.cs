using CCSWE.nanoFramework.WebServer.Http;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.WebServer.Samples
{
    internal class ExampleMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        // Middleware supports dependency injection
        public ExampleMiddleware(ILogger logger)
        {
            _logger = logger;
            _logger.LogInformation($"{nameof(ExampleMiddleware)}.ctor() called.");
        }

        public void Invoke(HttpContext context, RequestDelegate next)
        {
            // Implement your middleware logic here
            _logger.LogInformation($"{nameof(ExampleMiddleware)}.Invoke() called.");

            // Call the next middleware in the pipeline or short-circuit the pipeline by not calling next (ie: you handled the request)
            next(context);
        }
    }
}
