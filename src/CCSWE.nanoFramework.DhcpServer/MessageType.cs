using System;

namespace CCSWE.nanoFramework.DhcpServer
{
    /// <summary>
    /// The type of DHCP message.
    /// </summary>
    internal enum MessageType : byte
    {
        /// <summary>
        /// The message type operation has not been set.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// The discover (DHCPDISCOVER) message type.
        /// </summary>
        Discover = 1,

        /// <summary>
        /// The offer (DHCPOFFER) message type.
        /// </summary>
        Offer = 2,

        /// <summary>
        /// The request (DHCPREQUEST) message type.
        /// </summary>
        Request = 3,

        /// <summary>
        /// The decline (DHCPDECLINE) message type.
        /// </summary>
        Decline = 4,

        /// <summary>
        /// The ACK (DHCPACK) message type.
        /// </summary>
        Ack = 5,

        /// <summary>
        /// The NAK (DHCPNAK) message type.
        /// </summary>
        Nak = 6,

        /// <summary>
        /// The release (DHCPRELEASE) message type.
        /// </summary>
        Release = 7,

        /// <summary>
        /// The inform (DHCPINFORM) message type.
        /// </summary>
        Inform = 8,
    }

    internal static class MessageTypeExtensions
    {
        public static string AsString(this MessageType type)
        {
            return type switch
            {
                MessageType.NotSet => "UNKNOWN",
                MessageType.Discover => "DISCOVER",
                MessageType.Offer => "OFFER",
                MessageType.Request => "REQUEST",
                MessageType.Decline => "DECLINE",
                MessageType.Ack => "ACK",
                MessageType.Nak => "NAK",
                MessageType.Release => "RELEASE",
                MessageType.Inform => "INFORM",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
