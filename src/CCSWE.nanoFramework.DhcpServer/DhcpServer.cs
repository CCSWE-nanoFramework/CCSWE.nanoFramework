using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using CCSWE.nanoFramework.DhcpServer.Options;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.DhcpServer
{
    /// <summary>
    /// A simple DHCP server.
    /// </summary>
    public sealed class DhcpServer : IDisposable
    {
        private static readonly IPAddress DefaultSubnetMask = new(new byte[] { 255, 255, 255, 0 });

        // TODO: Replace with IPAddress.Broadcast when it's merged in
        private static readonly IPAddress Broadcast = new(new byte[] { 255, 255, 255, 255 });

        private const int ServerPort = 67;
        private const int ClientPort = 68;

        private static readonly IPEndPoint BroadcastEndpoint = new(Broadcast, ClientPort);

        private readonly IPAddressPool _addressPool;
        private Timer? _addressPoolTimer;
        private bool _disposed;
        private readonly object _lock = new();
        private readonly ILogger? _logger;
        private readonly OptionCollection _options = new();
        private Thread? _serverThread;
        private bool _started;
        private Socket? _requestSocket;
        private Socket? _responseSocket;

        /// <summary>
        /// Creates a new instance of <see cref="DhcpServer"/> that listens on the specified address.
        /// </summary>
        /// <param name="serverAddress">The address this server listens on.</param>
        /// <param name="logger">An optional <see cref="ILogger"/>.</param>
        /// <remarks>
        /// The <see cref="SubnetMask"/> will be set to 255.255.255.0.
        /// </remarks>
        public DhcpServer(IPAddress serverAddress, ILogger? logger = null): this(serverAddress, DefaultSubnetMask, logger) { }

        /// <summary>
        /// Creates a new instance of <see cref="DhcpServer"/> that listens on the specified address.
        /// </summary>
        /// <param name="serverAddress">The address this server listens on.</param>
        /// <param name="subnetMask">The subnet mask that will be assigned to clients.</param>
        /// <param name="logger">An optional <see cref="ILogger"/>.</param>
        /// <remarks>
        /// While the subnet mask can be specified the address pool will only support 254 addresses.
        /// </remarks>
        public DhcpServer(IPAddress serverAddress, IPAddress subnetMask, ILogger? logger = null)
        {
            _addressPool = new IPAddressPool(serverAddress);
            _logger = logger;

            ServerAddress = serverAddress;
            SubnetMask = subnetMask;
        }

        /// <summary>
        /// Finalizes the <see cref="DhcpServer"/>.
        /// </summary>
        ~DhcpServer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets the captive portal URL. If null or empty, this will be ignored.
        /// </summary>
        /// <remarks>A proper captive portal requires a valid SSL certificate and DNS resolution. See https://datatracker.ietf.org/doc/html/rfc8908</remarks>
        public string? CaptivePortalUrl
        {
            get => _options.GetOrDefault(OptionCode.CaptivePortal, string.Empty);
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _options.Remove(OptionCode.CaptivePortal);
                }
                else
                {
                    _options.Add(new StringOption(OptionCode.CaptivePortal, value!));
                }
            }
        }

        /// <summary>
        /// Gets or sets the DNS server. If set to null or 0.0.0.0, this will be ignored.
        /// </summary>
        public IPAddress? DnsServer
        {
            get
            {
                var dnsServer = _options.GetOrDefault(OptionCode.DomainNameServer, IPAddress.Any);
                return !IPAddress.Any.Equals(dnsServer) ? dnsServer : null;
            }
            set
            {
                if (value is null || IPAddress.Any.Equals(value))
                {
                    _options.Remove(OptionCode.DomainNameServer);
                }
                else
                {
                    _options.Add(new IPAddressOption(OptionCode.DomainNameServer, value));
                }
            }
        }

        /// <summary>
        /// Gets or sets the lease time.
        /// </summary>
        public TimeSpan LeaseTime { get; set; } = TimeSpan.FromHours(2);

        /// <summary>
        /// Gets the raw <see cref="OptionCollection"/>
        /// </summary>
        public OptionCollection Options => _options;

        private Socket Request
        {
            get
            {
                if (_requestSocket is null)
                {
                    lock (_lock)
                    {
                        _requestSocket ??= new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    }
                }

                return _requestSocket;
            }
        }

        private Socket Response
        {
            get
            {
                if (_responseSocket is null)
                {
                    lock (_lock)
                    {
                        _responseSocket ??= new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    }
                }

                return _responseSocket;
            }
        }

        /// <summary>
        /// Gets the server address.
        /// </summary>
        public IPAddress ServerAddress { get; }

        /// <summary>
        /// Gets the subnet mask.
        /// </summary>
        public IPAddress SubnetMask { get; }

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

            Stop();

            if (disposing)
            {

            }

            _disposed = true;
        }

        private static string FormatLogMessage(string message) => $"[{nameof(DhcpServer)}] {message}";

        private void HandleDiscoverMessage(Message request)
        {
            if (!request.GatewayIPAddress.Equals(IPAddress.Any))
            {
                // We only respond to requests on our subnet
                return;
            }

            var leaseTime = request.LeaseTime > TimeSpan.Zero && request.LeaseTime <= LeaseTime ? request.LeaseTime : LeaseTime;
            IPAddress? yourIPAddress = null;

            // If the requested IP address is leased to this client use it and the time remaining on the existing lease
            if (_addressPool.IsLeasedTo(request.RequestedIPAddress, request.HardwareAddressString, out var existingLease))
            {
                leaseTime = existingLease.Remaining;
                yourIPAddress = request.RequestedIPAddress;
            }

            yourIPAddress ??= _addressPool.GetAvailableAddress();

            if (yourIPAddress is not null)
            {
                SendResponse(MessageBuilder.CreateOffer(request, ServerAddress, yourIPAddress, SubnetMask, leaseTime, _options));
            }
            else
            {
                Log(LogLevel.Warning, "No more addresses available.");
            }
        }

        private void HandleInformMessage(Message request)
        {
            if (!_addressPool.IsLeasedTo(request.ClientIPAddress, request.HardwareAddressString))
            {
                return;
            }

            var response = MessageBuilder.CreateAck(request, ServerAddress, request.ClientIPAddress, SubnetMask, TimeSpan.Zero, _options);

            SendResponse(response, request.ClientIPAddress);
        }

        private void HandleReleaseMessage(Message message)
        {
            _addressPool.Release(message.ClientIPAddress, message.HardwareAddressString);
        }

        private void HandleRequestMessage(Message request)
        {
            var serverIdentifier = request.ServerIdentifier;
            if (serverIdentifier.Equals(IPAddress.Any))
            {
                if (request.ClientIPAddress.Equals(IPAddress.Any))
                {
#if DEBUG
                    Log(LogLevel.Trace, "Received REQUEST without ciaddr, client is INIT-REBOOT");
#endif

                    var lease = _addressPool.Request(request.RequestedIPAddress, request.HardwareAddressString, LeaseTime);

                    SendResponse(lease is null
                        ? MessageBuilder.CreateNak(request, ServerAddress)
                        : MessageBuilder.CreateAck(request, ServerAddress, lease.ClientAddress, SubnetMask, lease.Remaining, _options));
                }
                else
                {
#if DEBUG
                    Log(LogLevel.Trace, $"Received REQUEST with ciaddr, client is RENEWING or REBINDING");
#endif

                    var lease = _addressPool.Renew(request.RequestedIPAddress, request.HardwareAddressString);

                    // TODO: Should be sending unicast OR broadcast depending on RENEWING or REBINDING respectively
                    // The only difference from these two states is how the client sent the request unicast (RENEWING) or broadcast (REBINDING)
                    // The `flags` field has be broadcast bit but it does not always seem to get set based on these states (it's not directly related)
                    // So for that reason I'm opting to default to sending responses via broadcast

                    SendResponse(lease is not null && lease.Remaining > TimeSpan.Zero
                        ? MessageBuilder.CreateAck(request, ServerAddress, lease.ClientAddress, SubnetMask, lease.Remaining, _options)
                        : MessageBuilder.CreateNak(request, ServerAddress));
                }
            }
            else if (serverIdentifier.Equals(ServerAddress))
            {
#if DEBUG
                Log(LogLevel.Trace, "Received REQUEST with server identifier, client is SELECTING");
#endif

                var lease = _addressPool.Request(request.RequestedIPAddress, request.HardwareAddressString, LeaseTime);

                SendResponse(lease is not null && lease.Remaining > TimeSpan.Zero
                    ? MessageBuilder.CreateAck(request, ServerAddress, lease.ClientAddress, SubnetMask, lease.Remaining, _options)
                    : MessageBuilder.CreateNak(request, ServerAddress));
            }
        }

        private void Log(LogLevel logLevel, string message, Exception? exception = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (_logger is null)
            {
#if !DEBUG
                if (logLevel > LogLevel.Debug)
                {
#endif
                    Console.WriteLine(FormatLogMessage(message));
#if !DEBUG
                }
#endif
                return;
            }

            _logger.Log(logLevel, exception, FormatLogMessage(message));
        }

        private void SendResponse(Message message) => SendResponse(message, BroadcastEndpoint);

        private void SendResponse(Message message, IPAddress destination) => SendResponse(message, new IPEndPoint(destination, ClientPort));

        private void SendResponse(Message message, EndPoint destination)
        {
            Log(LogLevel.Trace, message.ToString());
            Response.SendTo(message.GetBytes(), destination);
        }

        private void ServerThread()
        {
            Log(LogLevel.Information, "Started");

            var buffer = new byte[768];

            while (_started)
            {
                try
                {
                    var bytes = Request.Receive(buffer);

                    if (bytes <= 0)
                    {
                        continue;
                    }

                    var message = MessageBuilder.Parse(buffer);

                    // Only respond to requests
                    if (message.Operation != Operation.BootRequest)
                    {
                        continue;
                    }

                    Log(LogLevel.Trace, message.ToString());

                    switch (message.MessageType)
                    {
                        case MessageType.Discover:
                        {
                            HandleDiscoverMessage(message);
                            break;
                        }
                        case MessageType.Inform:
                        {
                            HandleInformMessage(message);
                            break;
                        }
                        case MessageType.Release:
                        {
                            HandleReleaseMessage(message);
                            break;
                        }
                        case MessageType.Request:
                        {
                            HandleRequestMessage(message);
                            break;
                        }
                        default:
                        {
                            Log(LogLevel.Trace, $"Unhandled message type '{message.MessageType.AsString()}' received from host: {message.HostName}");
                            break;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log(LogLevel.Error, $"Unhandled exception: {exception.Message}", exception);
                }
            }

            Log(LogLevel.Information, "Stopped");
        }

        /// <summary>
        /// Starts the DHCP server.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if started successfully; otherwise <see langword="false"/>.
        /// </returns>
        public bool Start()
        {
            if (!_started)
            {
                lock (_lock)
                {
                    if (_started)
                    {
                        return true;
                    }

                    _started = true;

                    try
                    {
                        Request.Bind(new IPEndPoint(ServerAddress, ServerPort));

                        Response.Bind(new IPEndPoint(ServerAddress, 0));
                        Response.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.Broadcast, true);
                        Response.Connect(new IPEndPoint(Broadcast, ClientPort));

                        _addressPoolTimer = new Timer(_ => { _addressPool.Evict(); }, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));

                        _serverThread = new Thread(ServerThread);
                        _serverThread.Start();

                        return true;
                    }
                    catch (SocketException ex)
                    {
                        Log(LogLevel.Error, $"Socket exception occurred. Code: {ex.ErrorCode} Message: {ex.Message}", ex);
                    }
                    catch (Exception ex)
                    {
                        Log(LogLevel.Error, $"Exception occurred. Message: {ex.Message}", ex);
                    }

                    Stop();
                }
            }

            return _started;
        }

        /// <summary>
        /// Stops the DHCP server.
        /// </summary>
        public void Stop()
        {
            lock (_lock)
            {
                if (_addressPoolTimer is not null)
                {
                    try
                    {
                        _addressPoolTimer.Change(0, 0);
                        _addressPoolTimer.Dispose();
                        _addressPoolTimer = null;
                    }
                    catch (Exception e)
                    {
                        Log(LogLevel.Error, $"Error stopping address pool timer: {e.Message}", e);
                    }
                }

                if (_requestSocket is not null)
                {
                    try
                    {
                        _requestSocket.Close();
                        _requestSocket = null;
                    }
                    catch (Exception e)
                    {
                        Log(LogLevel.Error, $"Error closing request socket: {e.Message}", e);
                    }
                }

                if (_responseSocket is not null)
                {
                    try
                    {
                        _responseSocket.Close();
                        _responseSocket = null;
                    }
                    catch (Exception e)
                    {
                        Log(LogLevel.Error, $"Error closing response socket: {e.Message}", e);
                    }
                }

                _started = false;
            }
        }
    }
}
