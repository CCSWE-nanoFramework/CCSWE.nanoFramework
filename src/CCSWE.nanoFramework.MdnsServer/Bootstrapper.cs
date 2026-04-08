using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// Extension methods for <see cref="IMdnsServer"/>.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds an <see cref="IMdnsServer"/> with the specified <see cref="MdnsServerOptions"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configureOptions">An optional delegate to configure <see cref="MdnsServerOptions"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddMdnsServer(this IServiceCollection services, ConfigureMdnsServerOptions? configureOptions = null)
    {
        var options = new MdnsServerOptions();
        configureOptions?.Invoke(options);

        services.AddSingleton(typeof(IMdnsServer), typeof(MdnsServer));
        services.AddSingleton(typeof(MdnsServerOptions), options);

        return services;
    }
}
