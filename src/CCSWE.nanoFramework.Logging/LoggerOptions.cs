using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Logging
{
    /// <summary>
    /// Options used with <see cref="ILogger"/>.
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// The minimum <see cref="LogLevel"/> to log.
        /// </summary>
        public LogLevel MinLogLevel { get; set; }

        /// <summary>
        /// Create a new <see cref="LoggerOptions"/>.
        /// </summary>
        /// <param name="minLogLevel">The minimum <see cref="LogLevel"/> to log.</param>
        public LoggerOptions(LogLevel minLogLevel = LogLevel.Debug)
        {
            MinLogLevel = minLogLevel;
        }
    }

    /// <summary>
    /// An action for configuring the <see cref="ILogger"/>.
    /// </summary>
    public delegate void ConfigureLoggerOptions(LoggerOptions options);
}
