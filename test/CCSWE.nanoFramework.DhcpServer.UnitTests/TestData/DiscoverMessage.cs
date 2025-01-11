using System.Net;

namespace CCSWE.nanoFramework.DhcpServer.UnitTests.TestData
{
    internal static class DiscoverMessage
    {
        // DISCOVER (1258F400): C8-E2-65-21-D2-84 jake, flags: 0, requested: 192.168.4.2, server: 0.0.0.0, options: {61: ??, 60: ??, 55: {1,3,6,15,31,33,43,44,46,47,119,121,249,252}}

        public static readonly IPAddress ClientIPAddress = new(0);
        public const ushort Flags = 0;
        public static readonly IPAddress GatewayIPAddress = new(0);
        public const string HardwareAddressString = "C8-E2-65-21-D2-84";
        public const byte HardwareAddressLength = 6;
        public const byte HardwareAddressType = 1;
        public const byte Hops = 0;
        public const string HostName = "jake";
        public static readonly MessageType MessageType = MessageType.Discover;
        public static readonly Operation Operation = Operation.BootRequest;
        public static readonly IPAddress RequestedIPAddress = new(new byte[] { 192, 168, 4, 2 });
        public const ushort SecondsElapsed = 0;
        public static readonly IPAddress ServerIdentifier = new(0);
        public static readonly IPAddress ServerIPAddress = new(0);
        public const uint TransactionId = 307819520;
        public static readonly IPAddress YourIPAddress = new(0);

        public static readonly byte[] Data =
        {
            1, 1, 6, 0, 0, 244, 88, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 226, 101, 33,
            210, 132, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 99, 130, 83, 99, 53, 1, 1, 61, 7,
            1, 200, 226, 101, 33, 210, 132, 50, 4, 192, 168, 4, 2, 12, 4, 106, 97, 107, 101, 60, 8, 77, 83, 70, 84, 32,
            53, 46, 48, 55, 14, 1, 3, 6, 15, 31, 33, 43, 44, 46, 47, 119, 121, 249, 252, 255
        };
    }
}
