using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Identifies an action that supports the HTTP HEAD method.
    /// </summary>
    public class HttpHeadAttribute : HttpMethodAttribute
    {
        private static readonly string[] SupportedMethods = [Http.HttpMethods.Head];

        /// <summary>
        /// Creates a new <see cref="HttpHeadAttribute"/>.
        /// </summary>
        public HttpHeadAttribute() : base(SupportedMethods)
        {
        }

        /// <summary>
        /// Creates a new <see cref="HttpHeadAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpHeadAttribute(string template) : base(SupportedMethods, template)
        {
            Ensure.IsNotNull(template);
        }
    }
}
