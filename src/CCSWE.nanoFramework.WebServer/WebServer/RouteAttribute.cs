using System;
using CCSWE.nanoFramework.WebServer.Routing;

// ReSharper disable RedundantAttributeUsageProperty
namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Specifies an attribute route on a controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RouteAttribute : Attribute, IRouteTemplateProvider
    {
        /// <summary>
        /// Creates a new <see cref="RouteAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public RouteAttribute(string template)
        {
            Template = template ?? throw new ArgumentNullException(nameof(template));
        }

        /// <inheritdoc />
        public string Template { get; }
    }
}
