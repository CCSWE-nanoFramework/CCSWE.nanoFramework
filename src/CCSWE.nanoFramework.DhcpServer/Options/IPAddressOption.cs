using System;
using System.Net;

namespace CCSWE.nanoFramework.DhcpServer.Options
{
    /// <summary>
    /// Represents a DHCP option with a single <see cref="IPAddress"/> value.
    /// </summary>
    public class IPAddressOption : OptionBase
    {
        private IPAddress? _value;

        /// <summary>
        /// Creates a new <see cref="IPAddressOption"/> with the specified <paramref name="code"/> and <paramref name="data"/>.
        /// </summary>
        public IPAddressOption(byte code, byte[] data) : base(code, data)
        {
            if (data.Length != 4)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Creates a new <see cref="IPAddressOption"/> with the specified <paramref name="code"/> and <paramref name="data"/>.
        /// </summary>
        public IPAddressOption(OptionCode code, byte[] data) : this((byte)code, data)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IPAddressOption"/> with the specified <paramref name="code"/> and <paramref name="value"/>.
        /// </summary>
        public IPAddressOption(OptionCode code, IPAddress value) : this(code, Converter.GetBytes(value))
        {
            _value = value;
        }

        /// <summary>
        /// Gets the <see cref="IPAddress"/> set for this DHCP option.
        /// </summary>
        public IPAddress Deserialize()
        {
            return _value ??= Converter.GetIPAddress(Data);
        }

        internal static bool IsKnownOption(byte code) => IsKnownOption((OptionCode)code);

        internal static bool IsKnownOption(OptionCode code)
        {
            return code switch
            {
                OptionCode.RequestedIPAddress => true,
                OptionCode.ServerIdentifier => true,
                OptionCode.SubnetMask => true,
                _ => false
            };
        }

        /// <inheritdoc />
        public override string ToString() => ToString(Deserialize());
    }
}
