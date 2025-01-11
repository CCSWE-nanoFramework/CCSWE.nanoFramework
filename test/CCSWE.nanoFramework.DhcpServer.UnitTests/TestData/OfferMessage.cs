using System;
using System.Net;

namespace CCSWE.nanoFramework.DhcpServer.UnitTests.TestData
{
    internal static class OfferMessage
    {
        // OFFER(1258F400) : C8-E2-65-21-D2-84, flags: 0, yiaddr: 192.168.4.2, lease: 7200s, server: 192.168.4.1, options: {114: http://192.168.4.1/, 59: 01:45:00, 58: 01:00:00, 1: 255.255.255.0}

        public static readonly IPAddress ClientIPAddress = new(0);
        public const ushort Flags = 0;
        public static readonly IPAddress GatewayIPAddress = new(0);
        public const string HardwareAddressString = "C8-E2-65-21-D2-84";
        public const byte HardwareAddressLength = 6;
        public const byte HardwareAddressType = 1;
        public const byte Hops = 0;
        public const string HostName = "";
        public static readonly TimeSpan LeaseTime = TimeSpan.FromSeconds(7200);
        public static readonly MessageType MessageType = MessageType.Offer;
        public static readonly Operation Operation = Operation.BootReply;
        public static readonly IPAddress RequestedIPAddress = new(0);
        public const ushort SecondsElapsed = 0;
        public static readonly IPAddress ServerIdentifier = new(new byte[] { 192, 168, 4, 1 });
        public static readonly IPAddress ServerIPAddress = new(0);
        public static readonly IPAddress SubnetMask = new(new byte[] { 255, 255, 255, 0 });
        public const uint TransactionId = 307819520;
        public static readonly IPAddress YourIPAddress = new(new byte[] { 192, 168, 4, 2 });

        public static readonly byte[] Data =
        {
            2, 1, 6, 0, 0, 244, 88, 18, 0, 0, 0, 0, 0, 0, 0, 0, 192, 168, 4, 2, 0, 0, 0, 0, 0, 0, 0, 0, 200, 226, 101,
            33, 210, 132, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 99, 130, 83, 99, 114, 19,
            104, 116, 116, 112, 58, 47, 47, 49, 57, 50, 46, 49, 54, 56, 46, 52, 46, 49, 47, 59, 4, 156, 24, 0, 0, 58, 4,
            16, 14, 0, 0, 54, 4, 192, 168, 4, 1, 53, 1, 2, 1, 4, 255, 255, 255, 0, 51, 4, 32, 28, 0, 0, 255
        };
    }
}
