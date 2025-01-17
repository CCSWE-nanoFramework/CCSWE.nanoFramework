using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Logging
{
    /// <summary>
    /// A logger that prints to the console
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ConsoleLogger"/>
        /// </summary>
        /// <param name="loggerOptions">The logger options</param>
        public ConsoleLogger(LoggerOptions? loggerOptions = null) : this(string.Empty, loggerOptions)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConsoleLogger"/>
        /// </summary>
        /// <param name="loggerName">The logger name</param>
        /// <param name="loggerOptions">The logger options</param>
        // ReSharper disable once MergeConditionalExpression
        private ConsoleLogger(string loggerName, LoggerOptions? loggerOptions): this(loggerName, loggerOptions is not null ? loggerOptions.MinLogLevel : LogLevel.Debug)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ConsoleLogger"/>
        /// </summary>
        /// <param name="loggerName">The logger name</param>
        /// <param name="minLogLevel">The minimum <see cref="LogLevel"/></param>
        private ConsoleLogger(string loggerName, LogLevel minLogLevel = LogLevel.Debug)
        {
            LoggerName = loggerName;
            MinLogLevel = minLogLevel;
        }

        /// <summary>
        /// Name of the logger
        /// </summary>
        public string LoggerName { get; }

        /// <summary>
        /// Sets the minimum log level
        /// </summary>
        public LogLevel MinLogLevel { get; set; }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel) => logLevel >= MinLogLevel;

        /// <inheritdoc />
        public void Log(LogLevel logLevel, EventId eventId, string state, Exception? exception, MethodInfo? format)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string message;

            if (format is null)
            {
                message = exception is null ? state : $"{state} {exception}";
            }
            else
            {
                message = (string)format.Invoke(null, [LoggerName, logLevel, eventId, state, exception]);
            }

            Console.WriteLine(message);
        }

        /// <summary>
        /// Create a new <see cref="ConsoleLogger"/>.
        /// </summary>
        /// <returns>A new <see cref="ConsoleLogger"/>.</returns>
        public static ILogger Create(string loggerName, LoggerOptions? loggerOptions = null) => new ConsoleLogger(loggerName, loggerOptions);
    }
}
