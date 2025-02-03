using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.StaticFiles
{
    internal class StaticFileMiddleware: IMiddleware
    {
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly IFileProvider _fileProvider;

        public StaticFileMiddleware(IFileProvider fileProvider, IContentTypeProvider contentTypeProvider)
        {
            _contentTypeProvider = contentTypeProvider;
            _fileProvider = fileProvider;
        }

        public void Invoke(HttpContext context, RequestDelegate next)
        {
            // Do not process the request if the endpoint is already set
            if (context.Endpoint is not null)
            {
                next(context);
                return;
            }

            // Check if the request method is supported
            if (!HttpMethods.IsGet(context.Request.Method)) // TODO: Support HEAD && !HttpMethods.IsHead(context.Request.Method))
            {
                next(context);
                return;
            }

            // Do not serve static files if the content type is not known
            if (!_contentTypeProvider.TryGetContentType(context.Request.Path, out var contentType))
            {
                next(context);
                return;
            }

            // Check if the file exists
            var fileInfo = _fileProvider.GetFileInfo(context.Request.Path);
            if (!fileInfo.Exists)
            {
                next(context);
                return;
            }

            // Serve the file
            using var stream = fileInfo.CreateReadStream();
            context.Response.Write(stream, contentType);
        }
    }
}
