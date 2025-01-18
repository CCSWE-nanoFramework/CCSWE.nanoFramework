namespace CCSWE.nanoFramework.WebServer.Routing
{
    /// <summary>
    /// Interface that exposes a list of http methods that are supported by a provider.
    /// </summary>
    public interface IHttpMethodProvider
    {
        /// <summary>
        /// The list of http methods this action provider supports.
        /// </summary>
        string[] HttpMethods { get; }
    }
}
