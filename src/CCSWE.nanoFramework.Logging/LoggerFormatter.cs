using System;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Logging
{
    // TODO: Replace this with a per-sink formatter
    internal static class LoggerFormatter
    {
        public static string Formatter(string loggerName, LogLevel logLevel, EventId eventId, string state, Exception? exception)
        {
            var level = logLevel switch
            {
                LogLevel.Trace => "[T]",
                LogLevel.Debug => "[D]",
                LogLevel.Information => "[I]",
                LogLevel.Warning => "[W]",
                LogLevel.Error => "[E]",
                LogLevel.Critical => "[C]",
                LogLevel.None => string.Empty,
                _ => string.Empty,
            };

            var time = $"[{DateTime.UtcNow.TimeOfDay}]";
            var logger = string.IsNullOrEmpty(loggerName) ? string.Empty : $" ({loggerName})";
            var message = exception is null ? state : $"{state} {exception}";

            return $"{time} {level}{logger}: {message}";
        }

        public static void Initialize()
        {
            LoggerExtensions.MessageFormatter = typeof(LoggerFormatter).GetMethod(nameof(Formatter));
        }
    }
}
