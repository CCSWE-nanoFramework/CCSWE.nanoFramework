using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.FileStorage
{
    /// <summary>
    /// Extension methods for <see cref="FileStorage"/>.
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Adds <see cref="IFileStorage"/> to the <see cref="IServiceCollection"/>
        /// </summary>
        public static IServiceCollection AddFileStorage(this IServiceCollection services)
        {
            return services.AddSingleton(typeof(IFileStorage), typeof(FileStorage));
        }
    }
}
