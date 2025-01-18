namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Web server with Controllers support
    /// </summary>
    public interface IWebServer
    {
        /// <summary>
        /// Starts web server.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops web server.
        /// </summary>
        void Stop();
    }
}