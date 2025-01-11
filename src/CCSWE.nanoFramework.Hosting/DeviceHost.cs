using nanoFramework.Hosting;

namespace CCSWE.nanoFramework.Hosting
{
    /// <summary>
    /// Provides convenience methods for creating instances of <see cref="IHostBuilder"/> with pre-configured defaults.
    /// </summary>
    public static class DeviceHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceHostBuilder"/> class with pre-configured defaults.
        /// </summary>
        public static IHostBuilder CreateDefaultBuilder() => Internal.DeviceHost.CreateBuilder();
    }
}
