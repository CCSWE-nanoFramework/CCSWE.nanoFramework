namespace CCSWE.nanoFramework.WebServer.Cors
{
    // TODO: Consider making this public so that users can create their own policies.
    internal class CorsPolicy
    {
        public static readonly CorsPolicy AllowAny = new()
        {
            AccessControlAllowHeaders = CorsConstants.AnyHeader,
            AccessControlAllowMethods = CorsConstants.AnyMethod,
            AccessControlAllowOrigin = CorsConstants.AnyOrigin
        };

        public string AccessControlAllowHeaders { get; init; } = CorsConstants.AnyHeader;
        public string AccessControlAllowMethods { get; init; } = CorsConstants.AnyMethod;
        public string AccessControlAllowOrigin { get; init; } = CorsConstants.AnyOrigin;
    }
}
