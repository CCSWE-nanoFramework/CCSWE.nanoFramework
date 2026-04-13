using Iot.Device.MulticastDns.Entities;

namespace CCSWE.nanoFramework.MdnsServer.Internal;

/// <summary>
/// A typed alias for <see cref="Response"/> used internally by <see cref="MdnsServer"/>.
/// </summary>
internal sealed class MdnsResponse : Response
{
    public bool IsEmpty()
    {
        return _answers.Count == 0 && _additionals.Count == 0 && _servers.Count == 0;
    }
}
