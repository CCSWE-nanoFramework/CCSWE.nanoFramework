namespace CCSWE.nanoFramework.WebServer.Middleware
{
    internal delegate RequestDelegate CreateMiddlewareDelegate(RequestDelegate next);
}
