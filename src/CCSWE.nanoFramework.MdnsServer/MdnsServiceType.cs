namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// Provides common mDNS service type strings for services typically exposed by embedded devices.
/// </summary>
public static class MdnsServiceType
{
    /// <summary>HTTP web server over TCP (<c>_http._tcp.local</c>).</summary>
    public static readonly string Http = "_http._tcp.local";

    /// <summary>HTTPS web server over TCP (<c>_https._tcp.local</c>).</summary>
    public static readonly string Https = "_https._tcp.local";

    /// <summary>MQTT message broker over TCP (<c>_mqtt._tcp.local</c>).</summary>
    public static readonly string Mqtt = "_mqtt._tcp.local";

    /// <summary>Secure MQTT message broker over TCP (<c>_mqtts._tcp.local</c>).</summary>
    public static readonly string MqttSecure = "_mqtts._tcp.local";

    /// <summary>Network Time Protocol over UDP (<c>_ntp._udp.local</c>).</summary>
    public static readonly string Ntp = "_ntp._udp.local";
}
