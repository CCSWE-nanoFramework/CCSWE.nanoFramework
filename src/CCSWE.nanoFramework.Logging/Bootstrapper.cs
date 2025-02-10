using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Logging
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Adds an <see cref="ConsoleLogger"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        public static IServiceCollection AddLogging(this IServiceCollection services, ConfigureLoggerOptions? configureOptions = null)
        {
            var options = new LoggerOptions();

            if (configureOptions is null)
            {
                options.MinLogLevel = LogLevel.Debug;
            }

            configureOptions?.Invoke(options);

            services.TryAddSingleton(typeof(ILogger), typeof(ConsoleLogger));
            services.TryAddSingleton(typeof(ILoggerFactory), typeof(ConsoleLoggerFactory));
            services.TryAdd(new ServiceDescriptor(typeof(LoggerOptions), options));

            LoggerFormatter.Initialize();

            return services;
        }
    }
}
