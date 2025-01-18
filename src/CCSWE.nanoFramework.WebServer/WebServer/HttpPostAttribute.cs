using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Identifies an action that supports the HTTP POST method.
    /// </summary>
    public class HttpPostAttribute : HttpMethodAttribute
    {
        private static readonly string[] SupportedMethods = [Http.HttpMethods.Post];

        /// <summary>
        /// Creates a new <see cref="HttpPostAttribute"/>.
        /// </summary>
        public HttpPostAttribute() : base(SupportedMethods)
        {
        }

        /// <summary>
        /// Creates a new <see cref="HttpPostAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpPostAttribute(string template) : base(SupportedMethods, template)
        {
            Ensure.IsNotNull(template);
        }
    }
}
