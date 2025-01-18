using System;
using System.Net;
using System.Net.Sockets;

namespace CCSWE.nanoFramework.WebServer.Http.Internal
{
    // TODO: Add unit tests
    internal class HttpContext : Http.HttpContext
    {
        private readonly HttpListenerContext _context;

        public HttpContext(HttpListenerContext context, IServiceProvider requestServices)
        {
            _context = context;

            Request = new HttpRequest(context.Request);
            RequestServices = requestServices;
            Response = new HttpResponse(context.Response);
        }

        public override Http.HttpRequest Request { get; }

        public override IServiceProvider RequestServices { get; }

        public override Http.HttpResponse Response { get; }

        public override void Close()
        {
            Request.Close();
            Response.Close();

            try
            {
                _context.Close();
            }
            catch
            {
                // Move along, nothing to see here
            }
        }
    }
}
