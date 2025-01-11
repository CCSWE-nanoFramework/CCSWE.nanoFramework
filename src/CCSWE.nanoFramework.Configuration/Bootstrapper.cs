using System;
using CCSWE.nanoFramework.Configuration.Internal;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace CCSWE.nanoFramework.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="ConfigurationManager"/>.
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Adds a <see cref="ConfigurationManager"/>.
        /// </summary>
        public static IServiceCollection AddConfigurationManager(this IServiceCollection services, ConfigureConfigurationManagerOptions? configureOptions = null)
        {
            var options = new ConfigurationManagerOptions();

            configureOptions?.Invoke(options);

            //services.AddSingleton(typeof(ConfigurationManagerOptions), options);
            services.AddSingleton(typeof(IConfigurationManager), typeof(ConfigurationManager));

            if (options.UseInternalStorage)
            {
                services.AddSingleton(typeof(IConfigurationStorage), typeof(InternalConfigurationStorage));
            }

            return services;
        }

        /// <summary>
        /// Registers a configuration object.
        /// </summary>
        public static IServiceCollection BindConfiguration(this IServiceCollection services, string section, object defaults, IValidateConfiguration? validator = null)
        {
            return services.BindConfiguration(section, defaults.GetType(), defaults, validator);
        }

        /// <summary>
        /// Registers a configuration object.
        /// </summary>
        // TODO: Since I require defaults is there any need for this overload to be public?
        public static IServiceCollection BindConfiguration(this IServiceCollection services, string section, Type type, object defaults, IValidateConfiguration? validator = null)
        {
            return services.BindConfiguration(new ConfigurationDescriptor(section, type, defaults, validator));
        }

        private static IServiceCollection BindConfiguration(this IServiceCollection services, ConfigurationDescriptor descriptor)
        {
            services.AddSingleton(typeof(ConfigurationDescriptor), descriptor);

            return services;
        }
    }
}
