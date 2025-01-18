using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Identifies an action that supports the HTTP PUT method.
    /// </summary>
    public class HttpPutAttribute : HttpMethodAttribute
    {
        private static readonly string[] SupportedMethods = [Http.HttpMethods.Put];

        /// <summary>
        /// Creates a new <see cref="HttpPutAttribute"/>.
        /// </summary>
        public HttpPutAttribute() : base(SupportedMethods)
        {
        }

        /// <summary>
        /// Creates a new <see cref="HttpPutAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpPutAttribute(string template) : base(SupportedMethods, template)
        {
            Ensure.IsNotNull(template);
        }
    }
}
