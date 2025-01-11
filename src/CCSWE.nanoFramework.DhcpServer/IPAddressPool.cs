using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace CCSWE.nanoFramework.DhcpServer
{
    /// <summary>
    /// A class C (255.255.255.0) IP address pool
    /// </summary>
    internal class IPAddressPool
    {
        private const ushort AddressPoolSize = 254;

        private readonly Hashtable _leases = new(AddressPoolSize);
        private readonly object _lock = new();
        private readonly IPAddress _serverAddress;

        public IPAddressPool(IPAddress serverAddress)
        {
            _serverAddress = serverAddress;
            _leases.Add(GetKey(serverAddress), new IPAddressLease(serverAddress, nameof(DhcpServer), DateTime.MaxValue));
        }

        public int Available => AddressPoolSize - _leases.Count;

        public void Evict()
        {
            lock (_lock)
            {
                var keyCollection = _leases.Keys;
                var keys = new byte[keyCollection.Count];

                keyCollection.CopyTo(keys, 0);

                foreach (var key in keys)
                {
                    if (key <= 0)
                    {
                        continue;
                    }

                    if (_leases[key] is IPAddressLease lease && lease.IsExpired())
                    {
                        //Debug.WriteLine($"Evicting {lease.ClientAddress}");

                        _leases.Remove(key);
                    }
                }
            }
        }

        public IPAddress? GetAvailableAddress()
        {
            lock (_lock)
            {
                var targetAddress = _serverAddress.GetAddressBytes();

                // Always start the search at 1 for simplicity
                for (byte i = 1; i < 255; i++)
                {
                    targetAddress[3] = i;

                    if (!_leases.Contains(targetAddress[3]))
                    {
                        return new IPAddress(targetAddress);
                    }
                }

                return null;
            }
        }

        public IPAddressLease? GetLease(IPAddress address)
        {
            if (!IsValidAddress(address))
            {
                return null;
            }

            return _leases[GetKey(address)] as IPAddressLease;
        }
/*
        public IPAddressLease? GetLease(IPAddress clientAddress, string hardwareAddress)
        {
            var lease = GetLease(clientAddress);
            return lease?.HardwareAddress == hardwareAddress ? lease : null;
        }
*/
        private byte GetKey(IPAddress clientAddress)
        {
            var clientAddressBytes = clientAddress.GetAddressBytes();

            if (!IsValidAddress(clientAddressBytes, _serverAddress.GetAddressBytes()))
            {
                throw new ArgumentException();
            }

            return clientAddressBytes[3];
        }

        /// <summary>
        /// Checks if the IP address is leased to the specified hardware address.
        /// </summary>
        /// <param name="clientAddress">The IP address to check.</param>
        /// <param name="hardwareAddress">The hardware address to check.</param>
        /// <returns><see langword="true"/> if the <paramref name="clientAddress"/> is leased to the <paramref name="hardwareAddress"/>; otherwise <see langword="false"/>.</returns>
        public bool IsLeasedTo(IPAddress clientAddress, string hardwareAddress) => IsLeasedTo(clientAddress, hardwareAddress, out _);

        /// <summary>
        /// Checks if the IP address is leased to the specified hardware address.
        /// </summary>
        /// <param name="clientAddress">The IP address to check.</param>
        /// <param name="hardwareAddress">The hardware address to check.</param>
        /// <param name="lease">The matching <see cref="IPAddressLease"/> if leased.</param>
        /// <returns><see langword="true"/> if the <paramref name="clientAddress"/> is leased to the <paramref name="hardwareAddress"/>; otherwise <see langword="false"/>.</returns>
        public bool IsLeasedTo(IPAddress clientAddress, string hardwareAddress, [NotNullWhen(true)] out IPAddressLease? lease)
        {
            lease = null;

            if (!IsValidAddress(clientAddress))
            {
                return false;
            }

            lease = _leases[GetKey(clientAddress)] as IPAddressLease;

            if (lease is null)
            {
                return false;
            }

            if (lease.HardwareAddress != hardwareAddress)
            {
                lease = null;
            }

            return lease is not null;
        }

        private bool IsValidAddress(IPAddress clientAddress)
        {
            return IsValidAddress(clientAddress.GetAddressBytes(), _serverAddress.GetAddressBytes());
        }

        private static bool IsValidAddress(byte[] clientAddress, byte[] serverAddress)
        {

            return clientAddress[0] == serverAddress[0] && clientAddress[1] == serverAddress[1] && clientAddress[2] == serverAddress[2];
        }

        public void Release(IPAddress clientAddress, string hardwareAddress)
        {
            lock (_lock)
            {
                if (!IsLeasedTo(clientAddress, hardwareAddress))
                {
                    return;
                }

                _leases.Remove(GetKey(clientAddress));
            }
        }

        public IPAddressLease? Renew(IPAddress clientAddress, string hardwareAddress)
        {
            if (!IsValidAddress(clientAddress))
            {
                return null;
            }

            lock (_lock)
            {
                var existingLease = GetLease(clientAddress);
                if (existingLease is null)
                {
                    return null;
                }

                if (existingLease.HardwareAddress != hardwareAddress)
                {
                    return null;
                }

                existingLease.Renew();

                return existingLease;
            }
        }

        public IPAddressLease? Request(IPAddress clientAddress, string hardwareAddress, TimeSpan leaseTime)
        {
            if (!IsValidAddress(clientAddress))
            {
                return null;
            }

            lock (_lock)
            {
                var existingLease = GetLease(clientAddress);
                if (existingLease is not null)
                {
                    return existingLease.HardwareAddress == hardwareAddress ? existingLease : null;
                }

                var lease = new IPAddressLease(clientAddress, hardwareAddress, leaseTime);
                _leases[GetKey(clientAddress)] = lease;

                return lease;
            }
        }
    }
}
