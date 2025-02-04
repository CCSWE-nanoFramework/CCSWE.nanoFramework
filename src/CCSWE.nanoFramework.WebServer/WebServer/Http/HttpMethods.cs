using System;

namespace CCSWE.nanoFramework.WebServer.Http
{
    /// <summary>
    /// Contains methods to verify the request method of an HTTP request.
    /// </summary>
    public static class HttpMethods
    {
        // We are intentionally using 'static readonly' here instead of 'const'.
        // 'const' values would be embedded into each assembly that used them
        // and each consuming assembly would have a different 'string' instance.
        // Using .'static readonly' means that all consumers get these exact same
        // 'string' instance, which means the 'ReferenceEquals' checks below work
        // and allow us to optimize comparisons when these constants are used.

        // Please do NOT change these to 'const'
        /// <summary>
        /// HTTP "CONNECT" method.
        /// </summary>
        public static readonly string Connect = "CONNECT";
        /// <summary>
        /// HTTP "DELETE" method.
        /// </summary>
        public static readonly string Delete = "DELETE";
        /// <summary>
        /// HTTP "GET" method.
        /// </summary>
        public static readonly string Get = "GET";
        /// <summary>
        /// HTTP "HEAD" method.
        /// </summary>
        public static readonly string Head = "HEAD";
        /// <summary>
        /// HTTP "OPTIONS" method.
        /// </summary>
        public static readonly string Options = "OPTIONS";
        /// <summary>
        /// HTTP "PATCH" method.
        /// </summary>
        public static readonly string Patch = "PATCH";
        /// <summary>
        /// HTTP "POST" method.
        /// </summary>
        public static readonly string Post = "POST";
        /// <summary>
        /// HTTP "PUT" method.
        /// </summary>
        public static readonly string Put = "PUT";
        /// <summary>
        /// HTTP "TRACE" method.
        /// </summary>
        public static readonly string Trace = "TRACE";

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is CONNECT.
        /// </summary>
        /// <param name="method">The HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is CONNECT; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsConnect(string? method)
        {
            return method is not null && EqualsOptimized(Connect, method);
        }

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is DELETE.
        /// </summary>
        /// <param name="method">The HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is DELETE; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsDelete(string? method)
        {
            return method is not null && EqualsOptimized(Delete, method);
        }

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is GET.
        /// </summary>
        /// <param name="method">The  HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is GET; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsGet(string? method)
        {
            return method is not null && EqualsOptimized(Get, method);
        }

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is HEAD.
        /// </summary>
        /// <param name="method">The HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is HEAD; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsHead(string? method)
        {
            return method is not null && EqualsOptimized(Head, method);
        }

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is OPTIONS.
        /// </summary>
        /// <param name="method">The HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is OPTIONS; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsOptions(string? method)
        {
            return method is not null && EqualsOptimized(Options, method);
        }

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is PATCH.
        /// </summary>
        /// <param name="method">The HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is PATCH; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsPatch(string? method)
        {
            return method is not null && EqualsOptimized(Patch, method);
        }

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is POST.
        /// </summary>
        /// <param name="method">The HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is POST; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsPost(string? method)
        {
            return method is not null && EqualsOptimized(Post, method);
        }

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is PUT.
        /// </summary>
        /// <param name="method">The HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is PUT; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsPut(string? method)
        {
            return method is not null && EqualsOptimized(Put, method);
        }

        /// <summary>
        /// Returns a value that indicates if the HTTP request method is TRACE.
        /// </summary>
        /// <param name="method">The HTTP request method.</param>
        /// <returns>
        /// <see langword="true" /> if the method is TRACE; otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsTrace(string? method)
        {
            return method is not null && EqualsOptimized(Trace, method);
        }

        /// <summary>
        ///  Returns the equivalent static instance, or the original instance if none match. This conversion is optional but allows for performance optimizations when comparing method values elsewhere.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetCanonicalizedValue(string? method) => method switch
        {
            not null when IsGet(method) => Get,
            not null when IsPost(method) => Post,
            not null when IsPut(method) => Put,
            not null when IsDelete(method) => Delete,
            not null when IsOptions(method) => Options,
            not null when IsHead(method) => Head,
            not null when IsPatch(method) => Patch,
            not null when IsTrace(method) => Trace,
            not null when IsConnect(method) => Connect,
            not null => method,
            _ => throw new ArgumentNullException(nameof(method))
        };

        /// <summary>
        /// Returns a value that indicates if the HTTP methods are the same.
        /// </summary>
        /// <param name="methodA">The first HTTP request method to compare.</param>
        /// <param name="methodB">The second HTTP request method to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the methods are the same; otherwise, <see langword="false" />.
        /// </returns>
        public static bool Equals(string methodA, string methodB)
        {
            return ReferenceEquals(methodA, methodB) || string.Equals(methodA.ToUpper(), methodB.ToUpper());
        }

        private static bool EqualsOptimized(string httpMethod, string otherHttpMethod)
        {
            return ReferenceEquals(httpMethod, otherHttpMethod) || string.Equals(httpMethod, otherHttpMethod.ToUpper());
        }
    }
}
