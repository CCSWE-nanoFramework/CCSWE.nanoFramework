using System;

namespace CCSWE.nanoFramework.DhcpServer
{
    [Flags]
    internal enum MessageFlags: ushort
    {
        None = 0,
        Broadcast = 0x1
    }
}
