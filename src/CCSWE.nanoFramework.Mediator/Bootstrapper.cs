using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace CCSWE.nanoFramework.Mediator
{
    /// <summary>
    /// Extension methods for <see cref="AsyncMediator"/>.
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Adds an <see cref="AsyncMediator"/> with the specified <see cref="AsyncMediatorOptions"/>.
        /// </summary>
        public static IServiceCollection AddMediator(this IServiceCollection services, ConfigureAsyncMediatorOptions? configureOptions = null)
        {
            var options = new AsyncMediatorOptions();
            configureOptions?.Invoke(options);

            services.AddSingleton(typeof(IMediator), typeof(AsyncMediator));
            services.AddSingleton(typeof(AsyncMediatorOptions), options);

            return services;
        }
    }
}
