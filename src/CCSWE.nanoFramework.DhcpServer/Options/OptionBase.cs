namespace CCSWE.nanoFramework.DhcpServer.Options
{
    /// <summary>
    /// A base class for implementing <see cref="IOption"/>.
    /// </summary>
    public abstract class OptionBase: IOption
    {
        /// <summary>
        /// Creates a new <see cref="OptionBase"/> with the specified <paramref name="code"/> and <paramref name="data"/>.
        /// </summary>
        protected OptionBase(byte code, byte[] data): this(code, data, (byte)data.Length) { }

        /// <summary>
        /// Creates a new <see cref="OptionBase"/> with the specified <paramref name="code"/>, <paramref name="data"/>, and <paramref name="length"/>.
        /// </summary>
        protected OptionBase(byte code, byte[] data, byte length)
        {
            Code = code;
            Data = data;
            Length = length;
        }

        /// <summary>
        /// Creates a new <see cref="OptionBase"/> with the specified <paramref name="code"/> and <paramref name="data"/>.
        /// </summary>
        protected OptionBase(OptionCode code, byte[] data) : this((byte)code, data) { }


        /// <inheritdoc />
        public byte Code { get; }

        /// <inheritdoc />
        public byte[] Data { get; }

        /// <inheritdoc />
        public byte Length { get; }

        /// <inheritdoc />
        public ushort OptionLength => (ushort)(Length + 2);

        /// <inheritdoc />
        public byte[] GetBytes()
        {
            var data = new byte[OptionLength];
            data[0] = Code;
            data[1] = Length;

            Converter.CopyTo(Data, 0, data, 2, Length);

            return data;
        }

        /// <inheritdoc cref="object.ToString" />
        public abstract override string ToString();

        /// <summary>
        /// Provides common formatting for <see cref="OptionBase.ToString"/>.
        /// </summary>
        protected string ToString(object value) => $"{Code}: {value}";
    }
}
