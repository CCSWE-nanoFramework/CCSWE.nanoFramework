using System;

namespace CCSWE.nanoFramework.DhcpServer.Options
{
    /// <summary>
    /// Represents a DHCP option with a <see cref="TimeSpan"/> value.
    /// </summary>
    /// <remarks>The <see cref="TimeSpan"/> is serialized as positive seconds.</remarks>
    public class TimeSpanOption: OptionBase
    {
        private bool _deserialized;
        private TimeSpan _value;

        /// <summary>
        /// Creates a new <see cref="TimeSpanOption"/> with the specified <paramref name="code"/> and <paramref name="data"/>.
        /// </summary>
        public TimeSpanOption(byte code, byte[] data): base(code, data) { }

        /// <summary>
        /// Creates a new <see cref="TimeSpanOption"/> with the specified <paramref name="code"/> and <paramref name="data"/>.
        /// </summary>
        public TimeSpanOption(OptionCode code, byte[] data) : this((byte)code, data) { }

        /// <summary>
        /// Creates a new <see cref="TimeSpanOption"/> with the specified <paramref name="code"/> and <paramref name="value"/>.
        /// </summary>
        public TimeSpanOption(OptionCode code, TimeSpan value) : this(code, Converter.GetBytes(value))
        {
            _deserialized = true;
            _value = value;
        }

        /// <summary>
        /// Gets the <see cref="TimeSpan"/> set for this DHCP option.
        /// </summary>
        public TimeSpan Deserialize()
        {
            if (!_deserialized)
            {
                _deserialized = true;
                _value = Converter.GetTimeSpan(Data);
            }

            return _value;
        }

        internal static bool IsKnownOption(byte code) => IsKnownOption((OptionCode)code);

        internal static bool IsKnownOption(OptionCode code)
        {
            return code switch
            {
                OptionCode.LeaseTime => true,
                OptionCode.RebindingTime => true,
                OptionCode.RenewalTime => true,
                _ => false
            };
        }

        /// <inheritdoc />
        public override string ToString() => ToString(Deserialize());
    }
}
