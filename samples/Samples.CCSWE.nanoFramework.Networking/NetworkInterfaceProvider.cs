using System;
using System.Net.NetworkInformation;

namespace Samples.CCSWE.nanoFramework.Networking;

public static class NetworkInterfaceProvider
{
    public static NetworkInterface? GetInterface(NetworkInterfaceType interfaceType)
    {
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        foreach (var networkInterface in networkInterfaces)
        {
            if (networkInterface.NetworkInterfaceType == interfaceType)
            {
                return networkInterface;
            }
        }

        return null;
    }

    public static NetworkInterface RequireInterface(NetworkInterfaceType interfaceType)
    {
        return GetInterface(interfaceType) ?? throw new ArgumentException($"Network interface not found: {interfaceType}");
    }
}
