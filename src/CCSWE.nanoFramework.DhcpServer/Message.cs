using System;
using System.Net;
using System.Text;
using CCSWE.nanoFramework.DhcpServer.Options;

namespace CCSWE.nanoFramework.DhcpServer
{
    /*
       FIELD      OCTETS       DESCRIPTION
       -----      ------       -----------
       
       op            1  Message op code / message type.
                        1 = BOOTREQUEST, 2 = BOOTREPLY
       htype         1  Hardware address type, see ARP section in "Assigned
                        Numbers" RFC; e.g., '1' = 10mb ethernet.
       hlen          1  Hardware address length (e.g.  '6' for 10mb
                        ethernet).
       hops          1  Client sets to zero, optionally used by relay agents
                        when booting via a relay agent.
       xid           4  Transaction ID, a random number chosen by the
                        client, used by the client and server to associate
                        messages and responses between a client and a
                        server.
       secs          2  Filled in by client, seconds elapsed since client
                        began address acquisition or renewal process.
       flags         2  Flags (see figure 2).
       ciaddr        4  Client IP address; only filled in if client is in
                        BOUND, RENEW or REBINDING state and can respond
                        to ARP requests.
       yiaddr        4  'your' (client) IP address.
       siaddr        4  IP address of next server to use in bootstrap;
                        returned in DHCPOFFER, DHCPACK by server.
       giaddr        4  Relay agent IP address, used in booting via a
                        relay agent.
       chaddr       16  Client hardware address.
       sname        64  Optional server host name, null terminated string.
       file        128  Boot file name, null terminated string; "generic"
                        name or null in DHCPDISCOVER, fully qualified
                        directory-path name in DHCPOFFER.
       options     var  Optional parameters field.  See the options
                        documents for a list of defined options. 
    */

    /// <summary>
    /// Represents a DHCP message as specified in RFC 2131.
    /// </summary>
    /// <remarks>Specification https://datatracker.ietf.org/doc/html/rfc2131</remarks>
    internal class Message
    {
        private string? _hardwareAddressString;

        /// <summary>
        /// Gets or sets the operation (op) code.
        /// </summary>
        /// <remarks>
        /// Message op code / message type. 1 = BOOTREQUEST, 2 = BOOTREPLY
        /// </remarks>
        public Operation Operation { get; init; }

        /// <summary>
        /// Gets or sets the hardware address type (htype).
        /// </summary>
        /// <remarks>
        /// Hardware address type, see ARP section in "Assigned Numbers" RFC; e.g., '1' = 10mb ethernet.
        /// </remarks>
        public byte HardwareAddressType { get; init; }

        /// <summary>
        /// Gets or sets the hardware address length (hlen).
        /// </summary>
        /// <remarks>
        /// Hardware address length (e.g.  '6' for 10mb ethernet).
        /// </remarks>
        public byte HardwareAddressLength { get; init; }

        /// <summary>
        /// Gets or sets the hops.
        /// </summary>
        /// <remarks>Client sets to zero, optionally used by relay agents when booting via a relay agent.</remarks>
        public byte Hops { get; init; }

        /// <summary>
        /// Gets or sets the transaction ID (xid).
        /// </summary>
        /// <remarks>
        /// A random number chosen by the client, used by the client and server to associate messages and responses between a client and a server.
        /// </remarks>
        public uint TransactionId { get; init; }

        /// <summary>
        /// Gets or sets the seconds elapsed (secs).
        /// </summary>
        /// <remarks>
        /// Filled in by client, seconds elapsed since client began address acquisition or renewal process.
        /// </remarks>
        public ushort SecondsElapsed { get; init; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        public ushort Flags { get; init; }

        /// <summary>
        /// Gets or sets the client IP address (ciaddr).
        /// </summary>
        /// <remarks>
        /// Client IP address; only filled in if client is in BOUND, RENEW or REBINDING state and can respond to ARP requests.
        /// </remarks>
        public IPAddress ClientIPAddress { get; init; } = IPAddress.Any;

        /// <summary>
        /// Gets or sets the your IP address (yiaddr).
        /// </summary>
        /// <remarks>'your' (client) IP address.</remarks>
        public IPAddress YourIPAddress { get; init; } = IPAddress.Any;

        /// <summary>
        /// Gets or sets the server IP address (siaddr).
        /// </summary>
        /// <remarks>
        /// IP address of next server to use in bootstrap; returned in DHCPOFFER, DHCPACK by server.
        /// </remarks>
        public IPAddress ServerIPAddress { get; init; } = IPAddress.Any;

        /// <summary>
        /// Gets or sets the gateway IP address (giaddr).
        /// </summary>
        /// <remarks>
        /// Relay agent IP address, used in booting via a relay agent.
        /// </remarks>
        public IPAddress GatewayIPAddress { get; init; } = IPAddress.Any;

        /// <summary>
        /// Gets or sets the client hardware address (chaddr).
        /// </summary>
        public byte[] HardwareAddress { get; init; } = { };

        // NOT IMPLEMENTED: sname (ServerHostName) 64 octets
        // NOT IMPLEMENTED: file 128 octets

        /// <summary>
        /// Gets or sets the magic cookie.
        /// </summary>
        /// <remarks>
        /// The first four octets of the 'options' field of the DHCP message
        /// contain the (decimal) values 99, 130, 83 and 99, respectively (this
        /// is the same magic cookie as is defined in RFC 1497 [17]).
        /// </remarks>
        public byte[] MagicCookie { get; init; } = new byte[4];

        /// <summary>
        /// The raw options data.
        /// </summary>
        public OptionCollection Options { get; init; } = new();

        public string HardwareAddressString => _hardwareAddressString ??= BitConverter.ToString(HardwareAddress);

        public string HostName => Options.GetOrDefault(OptionCode.HostName, string.Empty);

        public TimeSpan LeaseTime => Options.GetOrDefault(OptionCode.LeaseTime, TimeSpan.Zero);

        public MessageType MessageType => ((MessageTypeOption)Options.Get(OptionCode.DhcpMessageType)!).Deserialize();

        public IPAddress RequestedIPAddress => Options.GetOrDefault(OptionCode.RequestedIPAddress, IPAddress.Any);

        public IPAddress ServerIdentifier => Options.GetOrDefault(OptionCode.ServerIdentifier, IPAddress.Any);

        /// <summary>
        /// Converts this <see cref="Message"/> to a <see cref="T:byte[]"/>.
        /// </summary>
        public byte[] GetBytes()
        {
            var data = new byte[MessageIndex.Options + Options.Length];

            Converter.CopyTo((byte)Operation, data, MessageIndex.Operation);
            Converter.CopyTo(HardwareAddressType, data, MessageIndex.HardwareAddressType);
            Converter.CopyTo(HardwareAddressLength, data, MessageIndex.HardwareAddressLength);
            Converter.CopyTo(Hops, data, MessageIndex.Hops);
            Converter.CopyTo(TransactionId, data, MessageIndex.TransactionId);
            Converter.CopyTo(SecondsElapsed, data, MessageIndex.SecondsElapsed);
            Converter.CopyTo(Flags, data, MessageIndex.Flags);
            Converter.CopyTo(ClientIPAddress, data, MessageIndex.ClientIPAddress);
            Converter.CopyTo(YourIPAddress, data, MessageIndex.YourIPAddress);
            Converter.CopyTo(ServerIPAddress, data, MessageIndex.ServerIPAddress);
            Converter.CopyTo(GatewayIPAddress, data, MessageIndex.GatewayIPAddress);
            Converter.CopyTo(HardwareAddress, data, MessageIndex.HardwareAddress);
            Converter.CopyTo(MagicCookie, data, MessageIndex.MagicCookie);
            Converter.CopyTo(Options.GetBytes(), data, MessageIndex.Options);

            return data;
        }

        public override string ToString()
        {
            var direction = Operation.BootReply == Operation ? "->" : "<-";
            var transactionId = TransactionId.ToString("X");
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{direction} {MessageType.AsString()} ({transactionId}): {HardwareAddressString}");

            if (!string.IsNullOrEmpty(HostName))
            {
                stringBuilder.Append($" {HostName}");
            }

            stringBuilder.Append($", flags: {Flags}");

            if (!ClientIPAddress.Equals(IPAddress.Any))
            {
                stringBuilder.Append($", ciaddr: {ClientIPAddress}");
            }

            if (!YourIPAddress.Equals(IPAddress.Any))
            {
                stringBuilder.Append($", yiaddr: {YourIPAddress}");
            }

            if (!ServerIPAddress.Equals(IPAddress.Any))
            {
                stringBuilder.Append($", siaddr: {ServerIPAddress}");
            }

            if (!GatewayIPAddress.Equals(IPAddress.Any))
            {
                stringBuilder.Append($", giaddr: {GatewayIPAddress}");
            }

            if (LeaseTime > TimeSpan.Zero)
            {
                stringBuilder.Append($", lease: {(int)LeaseTime.TotalSeconds}s");
            }

            if (!RequestedIPAddress.Equals(IPAddress.Any))
            {
                stringBuilder.Append($", requested: {RequestedIPAddress}");
            }

            stringBuilder.Append($", server: {ServerIdentifier}");

#if DEBUG
            var firstOption = true;
            foreach (var item in Options)
            {
                if (item is not IOption option)
                {
                    continue;
                }

                if ((OptionCode)option.Code is OptionCode.DhcpMessageType or OptionCode.HostName or OptionCode.LeaseTime or OptionCode.RequestedIPAddress or OptionCode.ServerIdentifier)
                {
                    continue;
                }

                stringBuilder.Append(", ");

                if (firstOption)
                {
                    stringBuilder.Append("options: {");
                }

                firstOption = false;
                stringBuilder.Append(option);
            }

            if (!firstOption)
            {
                stringBuilder.Append("}");
            }
#endif

            return stringBuilder.ToString();

        }
    }
}
