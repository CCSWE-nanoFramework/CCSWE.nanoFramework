namespace CCSWE.nanoFramework.WebServer.Http.Headers
{
    internal static class CorsConstants
    {
        /// <summary>
        /// The value for the Access-Control-Allow-Origin response header to allow all origins.
        /// </summary>
        public static readonly string AnyOrigin = "*";

        /// <summary>
        /// The value for the Access-Control-Allow-Headers response header to allow all headers.
        /// </summary>
        public static readonly string AnyHeader = "*";

        /// <summary>
        /// The value for the Access-Control-Allow-Methods response header to allow all methods.
        /// </summary>
        public static readonly string AnyMethod = "*";
    }
}
