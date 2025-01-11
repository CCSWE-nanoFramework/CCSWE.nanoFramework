using System;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.Hosting;

namespace CCSWE.nanoFramework.Hosting
{
    /// <summary>
    /// Extension methods for <see cref="IServiceProvider"/>.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Retrieve an array of <see cref="IDeviceInitializer"/> that have been registered with the <see cref="IServiceProvider"/>.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>An array of <see cref="IDeviceInitializer"/>.</returns>
        public static IDeviceInitializer[] GetDeviceInitializers(this IServiceProvider serviceProvider)
        {
            var objects = serviceProvider.GetServices(typeof(IDeviceInitializer));
            var deviceInitializers = new IDeviceInitializer[objects.Length];

            for (var i = 0; i < objects.Length; i++)
            {
                deviceInitializers[i] = (IDeviceInitializer)objects[i];
            }

            return deviceInitializers;
        }

        /// <summary>
        /// Retrieve an array of <see cref="IHostedService"/> that have been registered with the <see cref="IServiceProvider"/>.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>An array of <see cref="IHostedService"/>.</returns>
        // TODO: Move this to the nanoFramework.Hosting library?
        public static IHostedService[] GetHostedServices(this IServiceProvider serviceProvider)
        {
            var objects = serviceProvider.GetServices(typeof(IHostedService));
            var hostedServices = new IHostedService[objects.Length];

            for (var i = 0; i < objects.Length; i++)
            {
                hostedServices[i] = (IHostedService)objects[i];
            }

            return hostedServices;
        }
    }
}
