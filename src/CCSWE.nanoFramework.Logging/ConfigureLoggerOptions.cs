using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Logging
{
    /// <summary>
    /// An action for configuring the <see cref="ILogger"/>.
    /// </summary>
    public delegate void ConfigureLoggerOptions(LoggerOptions options);
}
