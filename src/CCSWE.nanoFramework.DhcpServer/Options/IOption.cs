namespace CCSWE.nanoFramework.DhcpServer.Options
{
    /// <summary>
    /// Represents a DHCP option.
    /// </summary>
    public interface IOption
    {
        /// <summary>
        /// The <see cref="OptionCode"/>.
        /// </summary>
        public byte Code { get; }

        /// <summary>
        /// Gets the data associated with the DHCP option.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Gets the length of <see cref="Data"/>.
        /// </summary>
        public byte Length { get; }

        /// <summary>
        /// Gets the length of the serialized <see cref="IOption"/>.
        /// </summary>
        public ushort OptionLength { get; }

        /// <summary>
        /// Converts the <see cref="IOption"/> to a <see cref="T:byte[]"/>.
        /// </summary>
        public byte[] GetBytes();

        /// <summary>
        /// Gets the string representation of this <see cref="IOption"/>.
        /// </summary>
        public string ToString();

        /* TODO: Report bug with default properties
        public uint OptionLength => (uint)(Length + 2);
        */

        /* TODO: And methods?
        public uint GetSerializedLength() => (uint)(Length + 2);

        public byte[] GetBytes()
        {
            var data = new byte[GetSerializedLength()];
            data[0] = Code;
            data[1] = Length;

            Array.Copy(Data, 0, data, 2, Length);

            return data;
        }
        */
    }
}
