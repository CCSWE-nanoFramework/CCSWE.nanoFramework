using System.Net;
using CCSWE.nanoFramework.DhcpServer.UnitTests.TestData;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.DhcpServer.UnitTests
{
    [TestClass]
    public class MessageBuilderTests
    {
        // TODO: Add Create* tests

        [TestMethod]
        public void Parse_handles_ack_message()
        {
            var actual = MessageBuilder.Parse(AckMessage.Data);

            Assert.AreEqual(AckMessage.Operation, actual.Operation, "Operation");
            Assert.AreEqual(AckMessage.HardwareAddressType, actual.HardwareAddressType, "HardwareAddressType");
            Assert.AreEqual(AckMessage.HardwareAddressLength, actual.HardwareAddressLength, "HardwareAddressLength");
            Assert.AreEqual(AckMessage.Hops, actual.Hops, "Hops");
            Assert.AreEqual(AckMessage.TransactionId, actual.TransactionId, "TransactionId");
            Assert.AreEqual(AckMessage.SecondsElapsed, actual.SecondsElapsed, "SecondsElapsed");
            Assert.AreEqual(AckMessage.Flags, actual.Flags, "Flags");
            Assert.AreEqual(AckMessage.ClientIPAddress, actual.ClientIPAddress, "ClientIPAddress");
            Assert.AreEqual(AckMessage.YourIPAddress, actual.YourIPAddress, "YourIPAddress");
            Assert.AreEqual(AckMessage.ServerIPAddress, actual.ServerIPAddress, "ServerIPAddress");
            Assert.AreEqual(AckMessage.GatewayIPAddress, actual.GatewayIPAddress, "GatewayIPAddress");
            Assert.AreEqual(AckMessage.HardwareAddressString, actual.HardwareAddressString, "HardwareAddressString");
            Assert.AreEqual(AckMessage.HostName, actual.HostName, "HostName");
            Assert.AreEqual(AckMessage.LeaseTime, actual.LeaseTime, "LeaseTime");
            Assert.AreEqual(AckMessage.MessageType, actual.MessageType, "MessageType");
            Assert.AreEqual(AckMessage.RequestedIPAddress, actual.RequestedIPAddress, "RequestedIPAddress");
            Assert.AreEqual(AckMessage.ServerIdentifier, actual.ServerIdentifier, "ServerIdentifier");

            MagicCookie.AssertValidCookie(actual.MagicCookie);

            Assert.IsTrue(actual.Options.Contains(1), "actual.Options.Contains(1)");
            Assert.IsTrue(actual.Options.Contains(58), "actual.Options.Contains(58)");
            Assert.IsTrue(actual.Options.Contains(59), "actual.Options.Contains(59)");
            Assert.IsTrue(actual.Options.Contains(114), "actual.Options.Contains(114)");

            Assert.AreEqual(AckMessage.SubnetMask, actual.Options.GetOrDefault(OptionCode.SubnetMask, new IPAddress(0)), "SubnetMask");
        }

        [TestMethod]
        public void Parse_handles_discover_message()
        {
            var actual = MessageBuilder.Parse(DiscoverMessage.Data);

            Assert.AreEqual(DiscoverMessage.Operation, actual.Operation, "Operation");
            Assert.AreEqual(DiscoverMessage.HardwareAddressType, actual.HardwareAddressType, "HardwareAddressType");
            Assert.AreEqual(DiscoverMessage.HardwareAddressLength, actual.HardwareAddressLength, "HardwareAddressLength");
            Assert.AreEqual(DiscoverMessage.Hops, actual.Hops, "Hops");
            Assert.AreEqual(DiscoverMessage.TransactionId, actual.TransactionId, "TransactionId");
            Assert.AreEqual(DiscoverMessage.SecondsElapsed, actual.SecondsElapsed, "SecondsElapsed");
            Assert.AreEqual(DiscoverMessage.Flags, actual.Flags, "Flags");
            Assert.AreEqual(DiscoverMessage.ClientIPAddress, actual.ClientIPAddress, "ClientIPAddress");
            Assert.AreEqual(DiscoverMessage.YourIPAddress, actual.YourIPAddress, "YourIPAddress");
            Assert.AreEqual(DiscoverMessage.ServerIPAddress, actual.ServerIPAddress, "ServerIPAddress");
            Assert.AreEqual(DiscoverMessage.GatewayIPAddress, actual.GatewayIPAddress, "GatewayIPAddress");
            Assert.AreEqual(DiscoverMessage.HardwareAddressString, actual.HardwareAddressString, "HardwareAddressString");
            Assert.AreEqual(DiscoverMessage.HostName, actual.HostName, "HostName");
            Assert.AreEqual(DiscoverMessage.MessageType, actual.MessageType, "MessageType");
            Assert.AreEqual(DiscoverMessage.RequestedIPAddress, actual.RequestedIPAddress, "RequestedIPAddress");
            Assert.AreEqual(DiscoverMessage.ServerIdentifier, actual.ServerIdentifier, "ServerIdentifier");

            MagicCookie.AssertValidCookie(actual.MagicCookie);
            
            Assert.IsTrue(actual.Options.Contains(55), "actual.Options.Contains(55)");
            Assert.IsTrue(actual.Options.Contains(60), "actual.Options.Contains(60)");
            Assert.IsTrue(actual.Options.Contains(61), "actual.Options.Contains(61)");
        }

        [TestMethod]
        public void Parse_handles_offer_message()
        {
            var actual = MessageBuilder.Parse(OfferMessage.Data);

            Assert.AreEqual(OfferMessage.Operation, actual.Operation, "Operation");
            Assert.AreEqual(OfferMessage.HardwareAddressType, actual.HardwareAddressType, "HardwareAddressType");
            Assert.AreEqual(OfferMessage.HardwareAddressLength, actual.HardwareAddressLength, "HardwareAddressLength");
            Assert.AreEqual(OfferMessage.Hops, actual.Hops, "Hops");
            Assert.AreEqual(OfferMessage.TransactionId, actual.TransactionId, "TransactionId");
            Assert.AreEqual(OfferMessage.SecondsElapsed, actual.SecondsElapsed, "SecondsElapsed");
            Assert.AreEqual(OfferMessage.Flags, actual.Flags, "Flags");
            Assert.AreEqual(OfferMessage.ClientIPAddress, actual.ClientIPAddress, "ClientIPAddress");
            Assert.AreEqual(OfferMessage.YourIPAddress, actual.YourIPAddress, "YourIPAddress");
            Assert.AreEqual(OfferMessage.ServerIPAddress, actual.ServerIPAddress, "ServerIPAddress");
            Assert.AreEqual(OfferMessage.GatewayIPAddress, actual.GatewayIPAddress, "GatewayIPAddress");
            Assert.AreEqual(OfferMessage.HardwareAddressString, actual.HardwareAddressString, "HardwareAddressString");
            Assert.AreEqual(OfferMessage.HostName, actual.HostName, "HostName");
            Assert.AreEqual(OfferMessage.LeaseTime, actual.LeaseTime, "LeaseTime");
            Assert.AreEqual(OfferMessage.MessageType, actual.MessageType, "MessageType");
            Assert.AreEqual(OfferMessage.RequestedIPAddress, actual.RequestedIPAddress, "RequestedIPAddress");
            Assert.AreEqual(OfferMessage.ServerIdentifier, actual.ServerIdentifier, "ServerIdentifier");

            MagicCookie.AssertValidCookie(actual.MagicCookie);

            Assert.IsTrue(actual.Options.Contains(1), "actual.Options.Contains(1)");
            Assert.IsTrue(actual.Options.Contains(58), "actual.Options.Contains(58)");
            Assert.IsTrue(actual.Options.Contains(59), "actual.Options.Contains(59)");
            Assert.IsTrue(actual.Options.Contains(114), "actual.Options.Contains(114)");

            Assert.AreEqual(OfferMessage.SubnetMask, actual.Options.GetOrDefault(OptionCode.SubnetMask, new IPAddress(0)), "SubnetMask");
        }

        [TestMethod]
        public void Parse_handles_request_message()
        {
            var actual = MessageBuilder.Parse(RequestMessage.Data);

            Assert.AreEqual(RequestMessage.Operation, actual.Operation, "Operation");
            Assert.AreEqual(RequestMessage.HardwareAddressType, actual.HardwareAddressType, "HardwareAddressType");
            Assert.AreEqual(RequestMessage.HardwareAddressLength, actual.HardwareAddressLength, "HardwareAddressLength");
            Assert.AreEqual(RequestMessage.Hops, actual.Hops, "Hops");
            Assert.AreEqual(RequestMessage.TransactionId, actual.TransactionId, "TransactionId");
            Assert.AreEqual(RequestMessage.SecondsElapsed, actual.SecondsElapsed, "SecondsElapsed");
            Assert.AreEqual(RequestMessage.Flags, actual.Flags, "Flags");
            Assert.AreEqual(RequestMessage.ClientIPAddress, actual.ClientIPAddress, "ClientIPAddress");
            Assert.AreEqual(RequestMessage.YourIPAddress, actual.YourIPAddress, "YourIPAddress");
            Assert.AreEqual(RequestMessage.ServerIPAddress, actual.ServerIPAddress, "ServerIPAddress");
            Assert.AreEqual(RequestMessage.GatewayIPAddress, actual.GatewayIPAddress, "GatewayIPAddress");
            Assert.AreEqual(RequestMessage.HardwareAddressString, actual.HardwareAddressString, "HardwareAddressString");
            Assert.AreEqual(RequestMessage.HostName, actual.HostName, "HostName");
            Assert.AreEqual(RequestMessage.MessageType, actual.MessageType, "MessageType");
            Assert.AreEqual(RequestMessage.RequestedIPAddress, actual.RequestedIPAddress, "RequestedIPAddress");
            Assert.AreEqual(RequestMessage.ServerIdentifier, actual.ServerIdentifier, "ServerIdentifier");

            MagicCookie.AssertValidCookie(actual.MagicCookie);

            Assert.IsTrue(actual.Options.Contains(55), "actual.Options.Contains(55)");
            Assert.IsTrue(actual.Options.Contains(60), "actual.Options.Contains(60)");
            Assert.IsTrue(actual.Options.Contains(61), "actual.Options.Contains(61)");
            Assert.IsTrue(actual.Options.Contains(81), "actual.Options.Contains(81)");
        }
    }
}