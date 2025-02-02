using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using CCSWE.nanoFramework.Threading.Internal;
using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Implements a web server
    /// </summary>
    public sealed class WebServer: IWebServer, IDisposable
    {
        private bool _cancellationRequested;
        private bool _disposed;
        private Thread? _listenerThread;
        private readonly object _lock = new();
        private readonly ILogger _logger;
        private readonly WebServerOptions _options;
        private readonly IServiceProvider _serviceProvider;
        private bool _started;
        private readonly ThreadPoolInternal _threadPool;

        /// <summary>
        /// Create a new instance of <see cref="WebServer"/>
        /// </summary>
        /// <param name="options">The <see cref="WebServerOptions"/> used to configure this <see cref="WebServer"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/> to log events to.</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> use to locate dependencies.</param>
        public WebServer(WebServerOptions options, ILogger logger, IServiceProvider serviceProvider)
        {
            _logger = new WebServerLogger(logger);
            _options = options;
            _serviceProvider = serviceProvider;
            _threadPool = new ThreadPoolInternal(32, 32);
            _threadPool.SetMinThreads(4);
        }

        /// <summary>
        /// The port the <see cref="WebServer"/> listens on.
        /// </summary>
        public ushort Port => _options.Port;

        private string Prefix => Protocol == HttpProtocol.Http ? "http" : "https";

        /// <summary>
        /// The <see cref="HttpProtocol"/> to use.
        /// </summary>
        public HttpProtocol Protocol => _options.Protocol;

        /// <summary>
        /// Finalizes the <see cref="WebServer"/>.
        /// </summary>
        ~WebServer()
        {
            Dispose(false);
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(WebServer));
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            lock (_lock)
            {
                if (_disposed)
                {
                    return;
                }

                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

            }

            Stop();

            _threadPool.Dispose();

            _disposed = true;
        }

        private void HandleRequest(object? state)
        {
            if (state is not HttpListenerContext context)
            {
                return;
            }

            using var requestScope = _serviceProvider.CreateScope();

            RequestPipeline.Execute(HttpContext.Create(context, requestScope.ServiceProvider));
        }

        private void ListenerThread()
        {
            var listener = new HttpListener(Prefix, Port);

            if (_options.Certificate is not null)
            {
                listener.HttpsCert = _options.Certificate;
            }

            listener.Start();

            _logger.LogDebug($"Web server started on port {Port}:");

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            // TODO: Need to check if interface is actually enabled and connected...
            foreach (var networkInterface in networkInterfaces)
            {
                // This is a bit of a hack, but it seems to work for now...
                if (string.IsNullOrEmpty(networkInterface.IPv4Address) || string.Equals("0.0.0.0", networkInterface.IPv4Address))
                {
                    continue;
                }

                _logger.LogDebug($" - {networkInterface.IPv4Address}");
            }

            try
            {
                while (!_cancellationRequested)
                {
                    var context = listener.GetContext();
                    if (context is null)
                    {
                        _logger.LogError("HttpListenerContext was null.");

                        return;
                    }

                    if (!_threadPool.QueueUserWorkItem(HandleRequest, context))
                    {
                        _logger.LogError("Max concurrent requests exceeded. Request ignored.");
                    }
                }
            }
            catch (ThreadAbortException)
            {
                // Move along, nothing to see here
            }

            listener.Stop();
        }

        /// <inheritdoc />
        public void Start()
        {
            CheckDisposed();

            if (_started)
            {
                return;
            }

            lock (_lock)
            {
                if (_started)
                {
                    return;
                }

                _cancellationRequested = false;

                _listenerThread = new Thread(ListenerThread);
                _listenerThread.Start();

                _started = true;
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            CheckDisposed();

            if (!_started)
            {
                return;
            }

            lock (_lock)
            {
                if (!_started)
                {
                    return;
                }

                _cancellationRequested = true;

                if (_listenerThread is not null)
                {
                    _listenerThread.Abort();
                    _listenerThread = null;
                }

                _started = false;
            }
        }
    }
}
