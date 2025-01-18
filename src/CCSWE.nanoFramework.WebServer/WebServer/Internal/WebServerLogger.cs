using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.WebServer.Internal
{
    internal class WebServerLogger: ILogger
    {
        private readonly ILogger _logger;

        public WebServerLogger(ILogger logger)
        {
            _logger = logger;
        }

        private static string FormatLogMessage(string message) => $"[{nameof(WebServer)}] {message}";

        public void Log(LogLevel logLevel, EventId eventId, string state, Exception? exception, MethodInfo? format)
        {
            if (string.IsNullOrEmpty(state))
            {
                return;
            }

            _logger.Log(logLevel, exception, FormatLogMessage(state));
        }

        public bool IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);
    }
}
