using System;
using System.Collections;
using Microsoft.Extensions.Logging;
using nanoFramework.Hosting;

namespace CCSWE.nanoFramework.Hosting.Internal
{
    internal sealed class DeviceHost : IHost
    {
        private IHostedService[] _hostedServices = null!;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Host"/>.
        /// </summary>
        public DeviceHost(IServiceProvider services, ILogger logger)
        {
            Ensure.IsNotNull(logger);
            Ensure.IsNotNull(services);

            _logger = logger;

            Services = services;
        }

        /// <inheritdoc />
        public IServiceProvider Services { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceHostBuilder"/> class with preconfigured defaults.
        /// </summary>
        /// <returns>The <see cref="DeviceHostBuilder"/>.</returns>
        public static DeviceHostBuilder CreateBuilder() => new();

        /// <inheritdoc />
        public void Dispose()
        {
            if (Services is IDisposable services)
            {
                services.Dispose();
            }
        }

        /// <inheritdoc />
        public void Start()
        {
            // TODO: Throw error if already started or no-op?

            var deviceInitializers = Services.GetDeviceInitializers();

            foreach (var deviceInitializer in deviceInitializers)
            {

                if (!deviceInitializer.Initialize())
                {
                    _logger.LogWarning($"{deviceInitializer.GetType().Name} returned false. Bailing!");
                    return;
                }
            }

            _hostedServices = Services.GetHostedServices();

            var exceptions = new ArrayList();

            for (var index = 0; index < _hostedServices.Length; index++)
            {
                var hostedService = _hostedServices[index];

                try
                {
                    hostedService.Start();

                    // TODO: Remove this when BackgroundService refactor is merged
                    if (hostedService is BackgroundService backgroundService)
                    {
                        backgroundService.ExecuteThread().Start();
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                var ex = new AggregateException("One or more hosted services failed to start.", exceptions);
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            var exceptions = new ArrayList();

            for (var index = _hostedServices.Length - 1; index >= 0; index--)
            {
                try
                {
                    _hostedServices[index].Stop();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(string.Empty, exceptions);
            }
        }
    }
}
