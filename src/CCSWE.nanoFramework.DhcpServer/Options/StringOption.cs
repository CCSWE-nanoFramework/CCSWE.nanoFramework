namespace CCSWE.nanoFramework.DhcpServer.Options
{
    /// <summary>
    /// Represents a DHCP option with a <see cref="string"/> value.
    /// </summary>
    /// <remarks>This option does not support strings that need to be null terminated.</remarks>
    public class StringOption: OptionBase
    {
        private string? _value;

        /// <summary>
        /// Creates a new <see cref="StringOption"/> with the specified <paramref name="code"/> and <paramref name="data"/>.
        /// </summary>
        public StringOption(byte code, byte[] data): base(code, data) { }

        /// <summary>
        /// Creates a new <see cref="StringOption"/> with the specified <paramref name="code"/> and <paramref name="data"/>.
        /// </summary>
        public StringOption(OptionCode code, byte[] data) : this((byte)code, data) { }

        /// <summary>
        /// Creates a new <see cref="StringOption"/> with the specified <paramref name="code"/> and <paramref name="value"/>.
        /// </summary>
        public StringOption(OptionCode code, string value) : this(code, Converter.GetBytes(value))
        {
            _value = value;
        }

        /// <summary>
        /// Gets the <see cref="string"/> set for this DHCP option.
        /// </summary>
        public string Deserialize()
        {
            return _value ??= Converter.GetString(Data);
        }

        internal static bool IsKnownOption(byte code) => IsKnownOption((OptionCode)code);

        internal static bool IsKnownOption(OptionCode code)
        {
            return code switch
            {
                OptionCode.HostName => true,
                _ => false
            };
        }

        /// <inheritdoc />
        public override string ToString() => ToString(Deserialize());
    }
}
