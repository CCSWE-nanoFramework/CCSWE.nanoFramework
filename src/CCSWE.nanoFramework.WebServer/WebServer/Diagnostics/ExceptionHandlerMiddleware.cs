using System;
using System.Net;
using CCSWE.nanoFramework.WebServer.Http;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.WebServer.Diagnostics
{
    internal class ExceptionHandlerMiddleware: IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(ILogger logger)
        {
            _logger = logger;
        }

        public void Invoke(HttpContext context, RequestDelegate next)
        {
            Exception? exception = null;

            try
            {
                next(context);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // TODO: Make this flexible by allowing consumer to implement non-default handling?
            if (exception is not null)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode(HttpStatusCode.InternalServerError, exception.Message);
                }

                _logger.LogError(exception, "Unhandled exception while processing request.");
            }
        }
    }
}
