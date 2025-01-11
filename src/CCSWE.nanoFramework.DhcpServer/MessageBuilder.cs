using System;
using System.Net;
using CCSWE.nanoFramework.DhcpServer.Options;

namespace CCSWE.nanoFramework.DhcpServer
{
    internal class MessageBuilder
    {
        public static Message CreateAck(Message request, IPAddress serverIdentifier, IPAddress yourIPAddress, IPAddress subnetMask, TimeSpan leaseTime, OptionCollection? options = null)
        {
            return CreateResponse(request, MessageType.Ack, serverIdentifier, yourIPAddress, subnetMask, leaseTime, options);
        }

        public static Message CreateNak(Message request, IPAddress serverIdentifier)
        {
            return CreateResponse(request, MessageType.Nak, serverIdentifier, IPAddress.Any);
        }

        public static Message CreateOffer(Message request, IPAddress serverIdentifier, IPAddress yourIPAddress, IPAddress subnetMask, TimeSpan leaseTime, OptionCollection? options = null)
        {
            return CreateResponse(request, MessageType.Offer, serverIdentifier, yourIPAddress, subnetMask, leaseTime, options);
        }

        /// <summary>
        /// Create a response <see cref="Message"/> from a request. Only <see cref="OptionCode.DhcpMessageType"/> and <see cref="OptionCode.ServerIdentifier"/> options are set.
        /// </summary>
        /// <param name="request">The request we are responding to.</param>
        /// <param name="responseType">The response <see cref="MessageType"/>.</param>
        /// <param name="serverIdentifier">The <see cref="IPAddress"/> of the server.</param>
        /// <param name="yourIPAddress">The <see cref="IPAddress"/> of the client.</param>
        /// <remarks>If the <paramref name="responseType"/> is <see cref="MessageType.Nak"/> then <paramref name="yourIPAddress"/> is ignored and will be set it <see cref="IPAddress.Any"/>.</remarks>
        private static Message CreateResponse(Message request, MessageType responseType, IPAddress serverIdentifier, IPAddress yourIPAddress)
        {
            var message = new Message
            {
                Operation = Operation.BootReply,
                HardwareAddressType = request.HardwareAddressType,
                HardwareAddressLength = request.HardwareAddressLength,
                Hops = 0,
                TransactionId = request.TransactionId,
                SecondsElapsed = 0,
                Flags = request.Flags,
                ClientIPAddress = IPAddress.Any,
                YourIPAddress = MessageType.Nak == responseType ? IPAddress.Any : yourIPAddress,
                ServerIPAddress = IPAddress.Any,
                GatewayIPAddress = request.GatewayIPAddress,
                HardwareAddress = request.HardwareAddress,
                MagicCookie = request.MagicCookie,
            };

            message.Options.Add(new MessageTypeOption(responseType));
            message.Options.Add(new IPAddressOption(OptionCode.ServerIdentifier, serverIdentifier));

            return message;
        }

        private static Message CreateResponse(Message request, MessageType responseType, IPAddress serverIdentifier, IPAddress yourIPAddress, IPAddress subnetMask, TimeSpan leaseTime, OptionCollection? options = null)
        {
            var message = CreateResponse(request, responseType, serverIdentifier, yourIPAddress);
            var requestType = request.MessageType;

            message.Options.Add(new IPAddressOption(OptionCode.SubnetMask, subnetMask));

            if (MessageType.Inform != requestType)
            {
                if (leaseTime <= TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(nameof(leaseTime));
                }

                message.Options.Add(new TimeSpanOption(OptionCode.LeaseTime, leaseTime));
                message.Options.Add(new TimeSpanOption(OptionCode.RenewalTime, TimeSpan.FromSeconds((long)(leaseTime.TotalSeconds * 0.5))));
                message.Options.Add(new TimeSpanOption(OptionCode.RebindingTime, TimeSpan.FromSeconds((long)(leaseTime.TotalSeconds * 0.875))));
            }

            if (options is not null)
            {
                foreach (var optionObject in options)
                {
                    if (optionObject is not IOption option)
                    {
                        continue;
                    }

                    if (!message.Options.Contains(option.Code) && IsOptionAllowedInResponse(requestType, responseType, option))
                    {
                        message.Options.Add(option);
                    }
                }
            }

            return message;
        }

        /// <summary>
        /// Checks if a given <see cref="IOption"/> is allowed in a response based on <paramref name="requestType"/> and/or <paramref name="responseType"/>.
        /// </summary>
        /// <param name="requestType">The request message type.</param>
        /// <param name="responseType">The response message type.</param>
        /// <param name="option">The option to check.</param>
        /// <returns><see langword="true"/> if the option is allowed; otherwise <see langword="false"/>.</returns>
        private static bool IsOptionAllowedInResponse(MessageType requestType, MessageType responseType, IOption option)
        {
            return (OptionCode)option.Code switch
            {
                OptionCode.ClassId => true,
                OptionCode.ClientId => MessageType.Nak == responseType,
                OptionCode.DhcpMessageType => true,
                OptionCode.LeaseTime => MessageType.Nak != responseType && MessageType.Inform != requestType,
                OptionCode.MaximumMessageSize => false,
                OptionCode.ParameterRequestList => false,
                OptionCode.ServerIdentifier => true,
                OptionCode.RequestedIPAddress => false,
                _ => MessageType.Nak != responseType
            };
        }

        public static Message Parse(byte[] data)
        {
            var message = new Message
            {
                Operation = (Operation)data[MessageIndex.Operation],
                HardwareAddressType = data[MessageIndex.HardwareAddressType],
                HardwareAddressLength = data[MessageIndex.HardwareAddressLength],
                Hops = data[MessageIndex.Hops],
                TransactionId = Converter.GetUInt32(data, MessageIndex.TransactionId),
                SecondsElapsed = Converter.GetUInt16(data, MessageIndex.SecondsElapsed),
                Flags = Converter.GetUInt16(data, MessageIndex.Flags),
                ClientIPAddress = Converter.GetIPAddress(data, MessageIndex.ClientIPAddress),
                YourIPAddress = Converter.GetIPAddress(data, MessageIndex.YourIPAddress),
                ServerIPAddress = Converter.GetIPAddress(data, MessageIndex.ServerIPAddress),
                GatewayIPAddress = Converter.GetIPAddress(data, MessageIndex.GatewayIPAddress),
                HardwareAddress = new byte[data[MessageIndex.HardwareAddressLength]],
                Options = OptionCollection.Parse(data)
            };

            Converter.CopyTo(data, MessageIndex.HardwareAddress, message.HardwareAddress, 0, message.HardwareAddress.Length);
            Converter.CopyTo(data, MessageIndex.MagicCookie, message.MagicCookie, 0, message.MagicCookie.Length);

            return message;
        }
    }
}
