using System;
using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Identifies an action that supports the HTTP OPTIONS method.
    /// </summary>
    public class HttpOptionsAttribute : HttpMethodAttribute
    {
        private static readonly string[] SupportedMethods = [Http.HttpMethods.Options];

        /// <summary>
        /// Creates a new <see cref="HttpOptionsAttribute"/>.
        /// </summary>
        public HttpOptionsAttribute() : base(SupportedMethods)
        {
        }

        /// <summary>
        /// Creates a new <see cref="HttpOptionsAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpOptionsAttribute(string template) : base(SupportedMethods, template)
        {
            ArgumentNullException.ThrowIfNull(template);
        }
    }
}
