using System;

// ReSharper disable RedundantAttributeUsageProperty
namespace CCSWE.nanoFramework.WebServer.Routing
{
    /// <summary>
    /// Identifies an action that supports a given set of HTTP methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class HttpMethodAttribute : Attribute, IHttpMethodProvider, IRouteTemplateProvider
    {
        /// <summary>
        /// Creates a new <see cref="HttpMethodAttribute"/> with the given
        /// set of HTTP methods.
        /// <param name="httpMethods">The set of supported HTTP methods. May not be null.</param>
        /// </summary>
        protected HttpMethodAttribute(string[] httpMethods): this(httpMethods, null)
        {

        }

        /// <summary>
        /// Creates a new <see cref="HttpMethodAttribute"/> with the given set of HTTP methods on the given route template.
        /// </summary>
        /// <param name="httpMethods">The set of supported methods. May not be null.</param>
        /// <param name="template">The route template.</param>
        protected HttpMethodAttribute(string[] httpMethods, string? template)
        {
            HttpMethods = httpMethods ?? throw new ArgumentNullException(nameof(httpMethods));
            Template = template;
        }
        
        /// <inheritdoc />
        public string[] HttpMethods { get; }

        /// <inheritdoc />
        public string? Template { get; }
    }
}
