namespace CCSWE.nanoFramework.Samples.Networking
{
    /// <summary>
    /// Holds the WiFi credentials used by <see cref="NetworkInitializer"/> to connect to a wireless network.
    /// </summary>
    public class NetworkConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NetworkConfiguration"/>.
        /// </summary>
        /// <param name="ssid">The SSID of the wireless network to connect to.</param>
        /// <param name="password">The password of the wireless network.</param>
        public NetworkConfiguration(string ssid, string password)
        {
            Password = password;
            Ssid = ssid;
        }

        /// <summary>Gets the wireless network password.</summary>
        public string Password { get; }

        /// <summary>Gets the wireless network SSID.</summary>
        public string Ssid { get; }
    }
}
