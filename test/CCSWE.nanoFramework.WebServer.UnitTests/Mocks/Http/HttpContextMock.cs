using System;
using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http
{
    internal class HttpContextMock : HttpContext
    {
        private HttpResponseMock? _response;

        public HttpContextMock(): this(HttpMethods.Get, string.Empty)
        {

        }

        public HttpContextMock(IServiceProvider services) : this(HttpMethods.Get, string.Empty, null, services)
        {

        }

        public HttpContextMock(string requestMethod, string requestUrl, HttpResponseMock? response = null, IServiceProvider? services = null) : this(new HttpRequestMock(requestMethod, requestUrl), response, services)
        {

        }

        public HttpContextMock(HttpRequestMock request, HttpResponseMock? response = null, IServiceProvider? services = null)
        {
            ArgumentNullException.ThrowIfNull(request);

            Request = request;
            RequestServices = services ?? new ServiceProviderMock();
            
            _response = response;
        }

        public override HttpRequest Request { get; }
        public override IServiceProvider RequestServices { get;}
        public override HttpResponse Response => _response ??= new HttpResponseMock(ResponseHasStarted);
        public bool ResponseHasStarted { get; init; }

        public bool ShouldSucceed { get; set; }
        public bool ShouldFail { get; set; }

        public override void Close()
        {
            Request.Close();
            Response.Close();
        }
    }
}
