using System.IO;
using System.Net;

namespace CCSWE.nanoFramework.WebServer.Http.Internal
{
    // TODO: Add unit tests
    internal class HttpResponse : Http.HttpResponse
    {
        private readonly HttpListenerResponse _response;

        public HttpResponse(HttpListenerResponse response)
        {
            _response = response;
        }

        public override Stream Body => _response.OutputStream;

        public override long ContentLength
        {
            get => _response.ContentLength64;
            set => _response.ContentLength64 = value;
        }

        public override string? ContentType
        {
            get => _response.ContentType;
            set => _response.ContentType = value;
        }

        public override bool HasStarted => _response.HasStarted;

        public override WebHeaderCollection Headers => _response.Headers;

        // TODO: Maybe remove this and handle it in the close (set KeepAlive to false if ContentLength = 0)
        public override bool KeepAlive 
        { 
            get => _response.KeepAlive;
            set => _response.KeepAlive = value;
        }

        public override bool SendChunked
        {
            get => _response.SendChunked;
            set => _response.SendChunked = value;
        }

        public override int StatusCode
        {
            get => _response.StatusCode;
            set => _response.StatusCode = value;
        }

        public override string? StatusDescription
        {
            get => _response.StatusDescription;
            set => _response.StatusDescription = value;
        }

        public override void Close()
        {
            try
            {
                _response.Close();
            }
            catch 
            {
                // Move along, nothing to see here
            }
        }
    }
}
