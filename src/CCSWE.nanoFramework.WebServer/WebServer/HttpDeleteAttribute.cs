using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Identifies an action that supports the HTTP DELETE method.
    /// </summary>
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        private static readonly string[] SupportedMethods = [Http.HttpMethods.Delete];

        /// <summary>
        /// Creates a new <see cref="HttpDeleteAttribute"/>.
        /// </summary>
        public HttpDeleteAttribute() : base(SupportedMethods)
        {
        }

        /// <summary>
        /// Creates a new <see cref="HttpDeleteAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public HttpDeleteAttribute(string template) : base(SupportedMethods, template)
        {
            Ensure.IsNotNull(template);
        }
    }
}
