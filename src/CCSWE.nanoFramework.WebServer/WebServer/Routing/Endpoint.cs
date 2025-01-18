using System.Collections;
using System.Reflection;
using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.Reflection;

namespace CCSWE.nanoFramework.WebServer.Routing
{
    // TODO: Handle typed parameters? The only benefit this gives me is route matching as the string can be parsed in the handling method
    internal class Endpoint
    {
        /// <summary>
        /// Represents an empty array of type <see cref="Endpoint"/>. This field is read-only.
        /// </summary>
        public static readonly Endpoint[] EmptyArray = [];

        public Endpoint(string httpMethod, MethodInfo methodInfo, string template, bool requireAuthentication)
        {
            Ensure.IsNotNullOrEmpty(httpMethod);
            Ensure.IsNotNull(methodInfo);
            Ensure.IsNotNullOrEmpty(template);

            HttpMethod = HttpMethods.GetCanonicalizedValue(httpMethod);
            MethodInfo = methodInfo;
            RequireAuthentication = requireAuthentication;
            RouteSegments = template.ToLower().Trim('/').Split('/');
            RouteTemplate = template;

            if (RouteTemplate.Contains("{"))
            {
                var parameterIndexes = new ArrayList();

                for (var i = 0; i < RouteSegments.Length; i++)
                {
                    if (RouteSegments[i].StartsWith("{"))
                    {
                        parameterIndexes.Add(i);
                    }
                }

                ParameterIndexes = (int[])parameterIndexes.ToArray(typeof(int));
            }
        }

        public string HttpMethod { get; }

        public MethodInfo MethodInfo { get; }

        public int[] ParameterIndexes { get; } = [];

        public bool RequireAuthentication { get; }

        public string[] RouteSegments { get; }

        public string RouteTemplate { get; }

        public override string ToString() => $"[{HttpMethod}] {RouteTemplate} => {MethodInfo.ToDisplayName()}";
    }
}
