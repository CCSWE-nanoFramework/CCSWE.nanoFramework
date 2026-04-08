using Iot.Device.MulticastDns.Entities;

namespace CCSWE.nanoFramework.MdnsServer.Internal;

/// <summary>
/// Extends <see cref="Response"/> to expose the additional records section per RFC 6762/6763.
/// </summary>
/// <remarks>
/// This is a temporary workaround until <c>Response.AddAdditional</c> is added to the upstream
/// <c>nanoFramework.Iot.Device.MulticastDns</c> library.
/// </remarks>
internal sealed class MdnsResponse : Response
{
    /// <summary>
    /// Adds a resource to the additional records section of the response.
    /// </summary>
    /// <param name="resource">The resource to add.</param>
    public void AddAdditional(Resource resource) => _additionals.Add(resource);
}
