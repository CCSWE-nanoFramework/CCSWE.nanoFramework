using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Logging
{
    /// <summary>
    /// Provides a <see cref="ConsoleLogger"/>.
    /// </summary>
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        private readonly LoggerOptions _loggerOptions;

        /// <summary>
        /// Create a new <see cref="ConsoleLoggerFactory"/>.
        /// </summary>
        /// <param name="loggerOptions">The <see cref="LoggerOptions"/> used when creating new <see cref="ILogger"/>.</param>
        public ConsoleLoggerFactory(LoggerOptions loggerOptions)
        {
            _loggerOptions = loggerOptions;
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return ConsoleLogger.Create(categoryName, _loggerOptions);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // nothing to do here
        }
    }
}
