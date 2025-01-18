namespace CCSWE.nanoFramework.WebServer.Routing
{
    /// <summary>
    /// Interface for attributes which can supply a route template for attribute routing.
    /// </summary>
    public interface IRouteTemplateProvider
    {
        /// <summary>
        /// The route template. May be <see langword="null"/>.
        /// </summary>
        string? Template { get; }
    }
}
