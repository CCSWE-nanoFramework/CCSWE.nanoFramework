
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace CCSWE.nanoFramework.Hosting
{
    /// <summary>
    /// Defines an interface that is run before <see cref="IHostedService.StartAsync(CancellationToken)"/> for device initialization.
    /// </summary>
    public interface IDeviceInitializer
    {
        /// <summary>
        /// Executed when the <see cref="IHost"/> is starting.
        /// </summary>
        /// <returns><see langword="true"/>, if initialization was successful; <see langword="false"/> if startup should be aborted.</returns>
        bool Initialize();
    }
}
