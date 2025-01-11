using System;
using System.Net;
using System.Threading;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.DhcpServer.UnitTests
{
    [TestClass]
    public class IPAddressPoolTests
    {
        //TODO: Add IsLeasedTo tests (needs to always "out" null unless hardware address matches

        private static IPAddress GenerateAddress(byte lastOctet) => new(new byte[] { 192, 168, 4, lastOctet });

        [TestMethod]
        public void Evict_removes_expired_leases()
        {
            var sut = new IPAddressPool(GenerateAddress(1));

            var available1 = sut.Available;
            for (byte i = 1; i < 100; i++)
            {
                sut.Request(GenerateAddress(i), $"MacAddress{i}", TimeSpan.FromMilliseconds(100));
            }

            var available2 = sut.Available;

            Assert.AreNotEqual(available1, available2);
            Thread.Sleep(1000);

            sut.Evict();

            Assert.AreEqual(available1, sut.Available);
        }

        [TestMethod]
        public void GetAvailableAddress_returns_IPAddress()
        {
            var sut = new IPAddressPool(GenerateAddress(1));
            var expect = GenerateAddress(2);

            var actual = sut.GetAvailableAddress();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void GetAvailableAddress_returns_null()
        {
            var sut = new IPAddressPool(GenerateAddress(1));

            for (byte i = 1; i < 255; i++)
            {
                sut.Request(GenerateAddress(i), $"MacAddress{i}", TimeSpan.FromMinutes(30));
            }

            var actual = sut.GetAvailableAddress();

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Release_removes_lease()
        {
            var sut = new IPAddressPool(GenerateAddress(1));

            var firstAddress = sut.GetAvailableAddress();
            Assert.IsNotNull(firstAddress);
            sut.Request(firstAddress!, "MacAddress1", TimeSpan.FromMinutes(30));

            var secondAddress = sut.GetAvailableAddress();
            Assert.IsNotNull(secondAddress);
            sut.Request(secondAddress!, "MacAddress2", TimeSpan.FromMinutes(30));

            sut.Release(firstAddress, "MacAddress1");
            var thirdAddress = sut.GetAvailableAddress();

            Assert.AreNotEqual(firstAddress, secondAddress, "firstAddress != secondAddress");
            Assert.AreEqual(firstAddress, thirdAddress, "firstAddress == thirdAddress");
        }

        [TestMethod]
        public void Renew_returns_lease()
        {
            var sut = new IPAddressPool(GenerateAddress(1));
            var clientAddress = GenerateAddress(2);
            var hardwareAddress = "MacAddress1";

            sut.Request(clientAddress, hardwareAddress, TimeSpan.FromMinutes(30));

            Assert.IsNotNull(sut.Renew(clientAddress, hardwareAddress));
        }

        [TestMethod]
        public void Renew_returns_null()
        {
            var sut = new IPAddressPool(GenerateAddress(1));
            var clientAddress = GenerateAddress(2);
            var hardwareAddress = "MacAddress1";

            var lease = sut.Renew(clientAddress, hardwareAddress);
            Assert.IsNull(lease, "Client address is not leased");

            sut.Request(clientAddress, hardwareAddress, TimeSpan.FromMinutes(30));

            Assert.IsNull(sut.Renew(clientAddress, "DifferentHardwareAddress"), "Hardware address is different");
        }

        [TestMethod]
        public void Request_returns_lease()
        {
            var sut = new IPAddressPool(GenerateAddress(1));

            Assert.IsNotNull(sut.Request(GenerateAddress(2), "MacAddress1", TimeSpan.FromMinutes(30)));
        }

        [TestMethod]
        public void Request_returns_null()
        {
            var sut = new IPAddressPool(GenerateAddress(1));
            var clientAddress = GenerateAddress(2);
            var hardwareAddress = "MacAddress1";

            Assert.IsNotNull(sut.Request(clientAddress, hardwareAddress, TimeSpan.FromMinutes(30)), "Address requested");
            Assert.IsNull(sut.Request(clientAddress, "DifferentHardwareAddress", TimeSpan.FromMinutes(30)), "Hardware address is different");
        }
    }
}
