namespace CCSWE.nanoFramework.DhcpServer
{
    /// <summary>
    /// Represents DHCP operation (op) codes used in a <see cref="Message"/>.
    /// </summary>
    internal enum Operation : byte
    {
        /// <summary>
        /// The operation code has not been set.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// Boot request (BOOTREQUEST) operation code.
        /// </summary>
        BootRequest = 1,

        /// <summary>
        /// Boot reply (BOOTREPLY) operation code.
        /// </summary>
        BootReply = 2
    }
}
