using System;
using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Identifies an action that supports the HTTP GET method.
    /// </summary>
    public class HttpGetAttribute : HttpMethodAttribute
    {
        private static readonly string[] SupportedMethods = [Http.HttpMethods.Get];

        /// <summary>
        /// Creates a new <see cref="HttpGetAttribute"/>.
        /// </summary>
        public HttpGetAttribute() : base(SupportedMethods)
        {
        }

        /// <summary>
        /// Creates a new <see cref="HttpGetAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpGetAttribute(string template) : base(SupportedMethods, template)
        {
            ArgumentNullException.ThrowIfNull(template);
        }
    }
}
