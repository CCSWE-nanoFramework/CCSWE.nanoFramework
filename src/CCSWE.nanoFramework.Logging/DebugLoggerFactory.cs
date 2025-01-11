using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Logging
{
    /// <summary>
    /// Provides a <see cref="DebugLogger"/>.
    /// </summary>
    public class DebugLoggerFactory : ILoggerFactory
    {
        private readonly LoggerOptions _loggerOptions;

        /// <summary>
        /// Create a new <see cref="DebugLoggerFactory"/>.
        /// </summary>
        /// <param name="loggerOptions">The <see cref="LoggerOptions"/> used when creating new <see cref="ILogger"/>.</param>
        public DebugLoggerFactory(LoggerOptions loggerOptions)
        {
            _loggerOptions = loggerOptions;
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return DebugLogger.Create(categoryName, _loggerOptions);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // nothing to do here
        }
    }
}
