using System;
using System.Net;

namespace CCSWE.nanoFramework.DhcpServer
{
    internal class IPAddressLease
    {
        /// <summary>
        /// Lazily initialized hash code
        /// </summary>
        private int _hashCode;

        public IPAddressLease(IPAddress clientAddress, string hardwareAddress, DateTime expiresAt) : this(clientAddress, hardwareAddress, expiresAt, TimeSpan.MaxValue)
        {

        }

        public IPAddressLease(IPAddress clientAddress, string hardwareAddress, TimeSpan leaseTime): this(clientAddress, hardwareAddress, DateTime.UtcNow + leaseTime, leaseTime)
        {

        }

        private IPAddressLease(IPAddress clientAddress, string hardwareAddress, DateTime expiresAt, TimeSpan leaseTime)
        {
            ClientAddress = clientAddress;
            ExpiresAt = expiresAt;
            HardwareAddress = hardwareAddress;
            LeaseTime = leaseTime;
        }

        public IPAddress ClientAddress { get; }

        public DateTime ExpiresAt { get; private set; }

        public string HardwareAddress { get; }

        public TimeSpan LeaseTime { get; }

        public TimeSpan Remaining
        {
            get
            {
                var remaining = ExpiresAt - DateTime.UtcNow;
                return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is IPAddressLease lease && Equals(lease);
        }

        public bool Equals(IPAddressLease other)
        {
            return ClientAddress.Equals(other.ClientAddress) && HardwareAddress.Equals(other.HardwareAddress);
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            if (_hashCode == 0)
            {
                _hashCode = ClientAddress.GetHashCode() ^ HardwareAddress.GetHashCode();
            }

            return _hashCode;
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }

        public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

        public void Renew() => ExpiresAt = DateTime.UtcNow + LeaseTime;
    }
}
