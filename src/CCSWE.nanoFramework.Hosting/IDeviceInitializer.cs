using nanoFramework.Hosting;

namespace CCSWE.nanoFramework.Hosting
{
    /// <summary>
    /// Defines an interface that is run before <see cref="IHostedService.Start()"/> for device initialization.
    /// </summary>
    public interface IDeviceInitializer
    {
        /// <summary>
        /// Executed when the <see cref="IHost"/> is starting.
        /// </summary>
        /// <returns><c>true</c>, if initialization was successful; <c>false</c> if startup should be aborted.</returns>
        bool Initialize();
    }
}
