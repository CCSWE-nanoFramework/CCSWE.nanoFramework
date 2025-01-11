namespace CCSWE.nanoFramework.DhcpServer
{
    /// <summary>
    /// Represents DHCP option codes as specified in RFC 2132 (and others).
    ///
    /// This enumeration is not a complete list.
    /// </summary>
    /// <remarks>
    /// Specification https://www.rfc-editor.org/rfc/rfc2132.txt.
    /// 
    /// IANA list https://www.iana.org/assignments/bootp-dhcp-parameters/bootp-dhcp-parameters.xhtml
    /// </remarks>
    public enum OptionCode : byte
    {
        /// <summary>
        /// The pad option can be used to cause subsequent fields to align on
        /// word boundaries.
        /// 
        /// The code for the pad option is 0, and its length is 1 octet.
        /// </summary>
        Pad = 0,

        /// <summary>
        /// The subnet mask option specifies the client's subnet mask as per RFC
        /// 950 [5].
        /// 
        /// If both the subnet mask and the router option are specified in a DHCP
        /// reply, the subnet mask option MUST be first.
        ///
        /// The code for the subnet mask option is 1, and its length is 4 octets.
        /// </summary>
        SubnetMask = 1,

        /// <summary>
        /// The time offset field specifies the offset of the client's subnet in
        /// seconds from Coordinated Universal Time (UTC).  The offset is
        /// expressed as a two's complement 32-bit integer.  A positive offset
        /// indicates a location east of the zero meridian and a negative offset
        /// indicates a location west of the zero meridian.
        /// 
        /// The code for the time offset option is 2, and its length is 4 octets.
        /// </summary>
        TimeOffset = 2,

        /// <summary>
        /// The router option specifies a list of IP addresses for routers on the
        /// client's subnet.  Routers SHOULD be listed in order of preference.
        /// 
        /// The code for the router option is 3.  The minimum length for the
        /// router option is 4 octets, and the length MUST always be a multiple
        /// of 4.
        /// </summary>
        Router = 3,

        /// <summary>
        /// The time server option specifies a list of RFC 868 [6] time servers
        /// available to the client.  Servers SHOULD be listed in order of
        /// preference.
        /// 
        /// The code for the time server option is 4.  The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        TimeServer = 4,

        /// <summary>
        /// The name server option specifies a list of IEN 116 [7] name servers
        /// available to the client.  Servers SHOULD be listed in order of
        /// preference.
        /// 
        /// The code for the name server option is 5.  The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        NameServer = 5,

        /// <summary>
        /// The domain name server option specifies a list of Domain Name System
        /// (STD 13, RFC 1035 [8]) name servers available to the client.  Servers
        /// SHOULD be listed in order of preference.
        /// 
        /// The code for the domain name server option is 6.  The minimum length
        /// for this option is 4 octets, and the length MUST always be a multiple
        /// of 4.
        /// </summary>
        DomainNameServer = 6,

        /// <summary>
        /// The log server option specifies a list of MIT-LCS UDP log servers
        /// available to the client.  Servers SHOULD be listed in order of
        /// preference.
        /// 
        /// The code for the log server option is 7.  The minimum length for this
        /// option is 4 octets, and the length MUST always be a multiple of 4.
        /// </summary>
        LogServer = 7,

        /// <summary>
        /// The cookie server option specifies a list of RFC 865 [9] cookie
        /// servers available to the client.  Servers SHOULD be listed in order
        /// of preference.
        /// 
        /// The code for the log server option is 8.  The minimum length for this
        /// option is 4 octets, and the length MUST always be a multiple of 4.
        /// </summary>
        CookieServer = 8,

        /// <summary>
        /// The LPR server option specifies a list of RFC 1179 [10] line printer
        /// servers available to the client.  Servers SHOULD be listed in order
        /// of preference.
        /// 
        /// The code for the LPR server option is 9.  The minimum length for this
        /// option is 4 octets, and the length MUST always be a multiple of 4.
        /// </summary>
        LinePrinterServer = 9,

        /// <summary>
        /// The Impress server option specifies a list of Imagen Impress servers
        /// available to the client.  Servers SHOULD be listed in order of
        /// preference.
        /// 
        /// The code for the Impress server option is 10.  The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        ImpressServer = 10,

        /// <summary>
        /// This option specifies a list of RFC 887 [11] Resource Location
        /// servers available to the client.  Servers SHOULD be listed in order
        /// of preference.
        ///
        /// The code for this option is 11.  The minimum length for this option
        /// is 4 octets, and the length MUST always be a multiple of 4.
        /// </summary>
        ResourceLocationServer = 11,

        /// <summary>
        /// This option specifies the name of the client.  The name may or may
        /// not be qualified with the local domain name (see section 3.17 for the
        /// preferred way to retrieve the domain name).  See RFC 1035 for
        /// character set restrictions.
        /// 
        /// The code for this option is 12, and its minimum length is 1.
        /// </summary>
        HostName = 12,

        /// <summary>
        /// This option specifies the length in 512-octet blocks of the default
        /// boot image for the client.  The file length is specified as an
        /// unsigned 16-bit integer.
        /// 
        /// The code for this option is 13, and its length is 2.
        /// </summary>
        BootFileSize = 13,

        /// <summary>
        /// This option specifies the path-name of a file to which the client's
        /// core image should be dumped in the event the client crashes.  The
        /// path is formatted as a character string consisting of characters from
        /// the NVT ASCII character set.
        /// 
        /// The code for this option is 14.  Its minimum length is 1.
        /// </summary>
        MeritDumpFile = 14,

        /// <summary>
        /// This option specifies the domain name that client should use when
        /// resolving hostnames via the Domain Name System.
        /// 
        /// The code for this option is 15.  Its minimum length is 1.
        /// </summary>
        DomainName = 15,

        /// <summary>This specifies the IP address of the client's swap server.
        /// 
        /// The code for this option is 16 and its length is 4.
        /// </summary>
        SwapServer = 16,

        /// <summary>
        /// This option specifies the path-name that contains the client's root
        /// disk.  The path is formatted as a character string consisting of
        /// characters from the NVT ASCII character set.
        /// 
        /// The code for this option is 17.  Its minimum length is 1.
        /// </summary>
        RootPath = 17,

        /// <summary>
        /// A string to specify a file, retrievable via TFTP, which contains
        /// information which can be interpreted in the same way as the 64-octet
        /// vendor-extension field within the BOOTP response, with the following
        /// exceptions:
        /// 
        /// - the length of the file is unconstrained;
        /// - all references to Tag 18 (i.e., instances of the BOOTP Extensions Path field) within the file are ignored.
        ///
        /// The code for this option is 18.  Its minimum length is 1.</summary>
        ExtensionsPath = 18,

        /// <summary>
        /// This option specifies whether the client should configure its IP
        /// layer for packet forwarding.  A value of 0 means disable IP
        /// forwarding, and a value of 1 means enable IP forwarding.
        /// 
        /// The code for this option is 19, and its length is 1.
        /// </summary>
        IpForwarding = 19,

        /// <summary>
        /// This option specifies whether the client should configure its IP
        /// layer to allow forwarding of datagrams with non-local source routes
        /// (see Section 3.3.5 of [4] for a discussion of this topic).  A value
        /// of 0 means disallow forwarding of such datagrams, and a value of 1
        /// means allow forwarding.
        /// 
        /// The code for this option is 20, and its length is 1.
        /// </summary>
        NonLocalSourceRouting = 20,

        /// <summary>
        /// This option specifies policy filters for non-local source routing.
        /// The filters consist of a list of IP addresses and masks which specify
        /// destination/mask pairs with which to filter incoming source routes.
        /// 
        /// Any source routed datagram whose next-hop address does not match one
        /// of the filters should be discarded by the client.
        ///
        /// The code for this option is 21.  The minimum length of this option is
        /// 8, and the length MUST be a multiple of 8.
        /// </summary>
        PolicyFilter = 21,

        /// <summary>
        /// This option specifies the maximum size datagram that the client
        /// should be prepared to reassemble.  The size is specified as a 16-bit
        /// unsigned integer.  The minimum value legal value is 576.
        /// 
        /// The code for this option is 22, and its length is 2.
        /// </summary>
        MaximumDatagramReassemblySize = 22,

        /// <summary>
        /// This option specifies the default time-to-live that the client should
        /// use on outgoing datagrams.  The TTL is specified as an octet with a
        /// value between 1 and 255.
        /// 
        /// The code for this option is 23, and its length is 1.
        /// </summary>
        DefaultIpTimeToLive = 23,

        /// <summary>
        /// This option specifies the timeout (in seconds) to use when aging Path
        /// MTU values discovered by the mechanism defined in RFC 1191 [12].  The
        /// timeout is specified as a 32-bit unsigned integer.
        /// 
        /// The code for this option is 24, and its length is 4.
        /// </summary>
        PathMtuAgingTimeout = 24,

        /// <summary>
        /// This option specifies a table of MTU sizes to use when performing
        /// Path MTU Discovery as defined in RFC 1191.  The table is formatted as
        /// a list of 16-bit unsigned integers, ordered from smallest to largest.
        /// The minimum MTU value cannot be smaller than 68.
        /// 
        /// The code for this option is 25.  Its minimum length is 2, and the
        /// length MUST be a multiple of 2.
        /// </summary>
        PathMtuPlateauTable = 25,

        /// <summary>
        /// This option specifies the MTU to use on this interface.  The MTU is
        /// specified as a 16-bit unsigned integer.  The minimum legal value for
        /// the MTU is 68.
        /// 
        /// The code for this option is 26, and its length is 2.
        /// </summary>
        InterfaceMtu = 26,

        /// <summary>
        /// This option specifies whether or not the client may assume that all
        /// subnets of the IP network to which the client is connected use the
        /// same MTU as the subnet of that network to which the client is
        /// directly connected.  A value of 1 indicates that all subnets share
        /// the same MTU.  A value of 0 means that the client should assume that
        /// some subnets of the directly connected network may have smaller MTUs.
        ///
        /// The code for this option is 27, and its length is 1.
        /// </summary>
        AllSubnetsAreLocal = 27,

        /// <summary>
        /// This option specifies the broadcast address in use on the client's
        /// subnet.  Legal values for broadcast addresses are specified in
        /// section 3.2.1.3 of [4].
        /// 
        /// The code for this option is 28, and its length is 4.
        /// </summary>
        BroadcastAddress = 28,

        /// <summary>
        /// This option specifies whether or not the client should perform subnet
        /// mask discovery using ICMP.  A value of 0 indicates that the client
        /// should not perform mask discovery.  A value of 1 means that the
        /// client should perform mask discovery.
        /// 
        /// The code for this option is 29, and its length is 1.
        /// </summary>
        PerformMaskDiscovery = 29,

        /// <summary>
        /// This option specifies whether or not the client should respond to
        /// subnet mask requests using ICMP.  A value of 0 indicates that the
        /// client should not respond.  A value of 1 means that the client should
        /// respond.
        /// 
        /// The code for this option is 30, and its length is 1.
        /// </summary>
        MaskSupplier = 30,

        /// <summary>
        /// This option specifies whether or not the client should solicit
        /// routers using the Router Discovery mechanism defined in RFC 1256
        /// [13].  A value of 0 indicates that the client should not perform
        /// router discovery.  A value of 1 means that the client should perform
        /// router discovery.
        /// 
        /// The code for this option is 31, and its length is 1.
        /// </summary>
        PerformRouterDiscovery = 31,

        /// <summary>
        /// This option specifies the address to which the client should transmit
        /// router solicitation requests.
        /// 
        /// The code for this option is 32, and its length is 4.
        /// </summary>
        RouterSolicitationAddressOption = 32,

        /// <summary>
        /// This option specifies a list of static routes that the client should
        /// install in its routing cache.  If multiple routes to the same
        /// destination are specified, they are listed in descending order of
        /// priority.
        /// 
        /// The routes consist of a list of IP address pairs.  The first address
        /// is the destination address, and the second address is the router for
        /// the destination.
        /// 
        /// The default route (0.0.0.0) is an illegal destination for a static
        /// route.  See section 3.5 for information about the router option.
        /// 
        /// The code for this option is 33.  The minimum length of this option is
        /// 8, and the length MUST be a multiple of 8.
        /// </summary>
        StaticRoute = 33,

        /// <summary>
        /// This option specifies whether or not the client should negotiate the
        /// use of trailers (RFC 893 [14]) when using the ARP protocol.  A value
        /// of 0 indicates that the client should not attempt to use trailers.  A
        /// value of 1 means that the client should attempt to use trailers.
        /// 
        /// The code for this option is 34, and its length is 1.
        /// </summary>
        TrailerEncapsulation = 34,

        /// <summary>
        /// This option specifies the timeout in seconds for ARP cache entries.
        /// The time is specified as a 32-bit unsigned integer.
        /// 
        /// The code for this option is 35, and its length is 4.
        /// </summary>
        ArpCacheTimeout = 35,

        /// <summary>
        /// This option specifies whether or not the client should use Ethernet
        /// Version 2 (RFC 894 [15]) or IEEE 802.3 (RFC 1042 [16]) encapsulation
        /// if the interface is an Ethernet.  A value of 0 indicates that the
        /// client should use RFC 894 encapsulation.  A value of 1 means that the
        /// client should use RFC 1042 encapsulation.
        ///
        /// The code for this option is 36, and its length is 1.
        /// </summary>
        EthernetEncapsulation = 36,

        /// <summary>
        /// This option specifies the default TTL that the client should use when
        /// sending TCP segments.  The value is represented as an 8-bit unsigned
        /// integer.  The minimum value is 1.
        /// 
        /// The code for this option is 37, and its length is 1.
        /// </summary>
        TcpDefaultTtl = 37,

        /// <summary>
        /// This option specifies the interval (in seconds) that the client TCP
        /// should wait before sending a keepalive message on a TCP connection.
        /// The time is specified as a 32-bit unsigned integer.  A value of zero
        /// indicates that the client should not generate keepalive messages on
        /// connections unless specifically requested by an application.
        /// 
        /// The code for this option is 38, and its length is 4.
        /// </summary>
        TcpKeepAliveInterval = 38,

        /// <summary>
        /// This option specifies the whether or not the client should send TCP
        /// keepalive messages with a octet of garbage for compatibility with
        /// older implementations.  A value of 0 indicates that a garbage octet
        /// should not be sent. A value of 1 indicates that a garbage octet
        /// should be sent.
        ///
        /// The code for this option is 39, and its length is 1.</summary>
        TcpKeepAliveGarbage = 39,

        /// <summary>
        /// This option specifies the name of the client's NIS [17] domain.  The
        /// domain is formatted as a character string consisting of characters
        /// from the NVT ASCII character set.
        /// 
        /// The code for this option is 40.  Its minimum length is 1.
        /// </summary>
        NetworkInformationServiceDomain = 40,

        /// <summary>
        /// This option specifies a list of IP addresses indicating NIS servers
        /// available to the client.  Servers SHOULD be listed in order of
        /// preference.
        /// 
        /// The code for this option is 41.  Its minimum length is 4, and the
        /// length MUST be a multiple of 4.
        /// </summary>
        NetworkInformationServers = 41,

        /// <summary>
        /// This option specifies a list of IP addresses indicating NTP [18]
        /// servers available to the client.  Servers SHOULD be listed in order
        /// of preference.
        /// 
        /// The code for this option is 42.  Its minimum length is 4, and the
        /// length MUST be a multiple of 4.
        /// </summary>
        NetworkTimeProtocolServers = 42,

        /// <summary>
        /// The NetBIOS name server (NBNS) option specifies a list of RFC
        /// 1001/1002 [19] [20] NBNS name servers listed in order of preference.
        /// 
        /// The code for this option is 44.  The minimum length of the option is
        /// 4 octets, and the length must always be a multiple of 4.
        /// </summary>
        NetBiosTcpNameServer = 44,

        /// <summary>
        /// The NetBIOS datagram distribution server (NBDD) option specifies a
        /// list of RFC 1001/1002 NBDD servers listed in order of preference. The
        /// code for this option is 45.  The minimum length of the option is 4
        /// octets, and the length must always be a multiple of 4.
        /// </summary>
        NetBiosTcpDatagramDistributionServer = 45,

        /// <summary>
        /// The NetBIOS node type option allows NetBIOS over TCP/IP clients which
        /// are configurable to be configured as described in RFC 1001/1002.  The
        /// value is specified as a single octet which identifies the client type.
        ///
        /// The code for this option is 46.  The length of this option is always 1.
        /// </summary>
        NetBiosTcpNodeType = 46,

        /// <summary>
        /// The NetBIOS scope option specifies the NetBIOS over TCP/IP scope
        /// parameter for the client as specified in RFC 1001/1002. See [19],
        /// [20], and [8] for character-set restrictions.
        /// 
        /// The code for this option is 47.  The minimum length of this option is 1.
        /// </summary>
        NetBiosTcpScope = 47,

        /// <summary>
        /// This option specifies a list of X Window System [21] Font servers
        /// available to the client. Servers SHOULD be listed in order of
        /// preference.
        /// 
        /// The code for this option is 48.  The minimum length of this option is
        /// 4 octets, and the length MUST be a multiple of 4.
        /// </summary>
        XWindowSystemFontServer = 48,

        /// <summary>
        /// This option specifies a list of IP addresses of systems that are
        /// running the X Window System Display Manager and are available to the
        /// client.
        /// 
        /// Addresses SHOULD be listed in order of preference.
        ///
        /// The code for the this option is 49. The minimum length of this option
        /// is 4, and the length MUST be a multiple of 4.
        /// </summary>
        XWindowSystemDisplayManager = 49,

        /// <summary>
        /// This option is used in a client request (DHCPDISCOVER) to allow the
        /// client to request that a particular IP address be assigned.
        /// 
        /// The code for this option is 50, and its length is 4.
        /// </summary>
        RequestedIPAddress = 50,

        /// <summary>
        /// This option is used in a client request (DHCPDISCOVER or DHCPREQUEST)
        /// to allow the client to request a lease time for the IP address.  In a
        /// server reply (DHCPOFFER), a DHCP server uses this option to specify
        /// the lease time it is willing to offer.
        /// 
        /// The time is in units of seconds, and is specified as a 32-bit
        /// unsigned integer.
        /// 
        /// The code for this option is 51, and its length is 4.
        /// </summary>
        LeaseTime = 51,

        /// <summary>
        /// This option is used to convey the type of the DHCP message.  The code
        /// for this option is 53, and its length is 1.  Legal values for this
        /// option are defined in <see cref="MessageType"/>.
        /// </summary>
        DhcpMessageType = 53,

        /// <summary>
        /// This option is used in DHCPOFFER and DHCPREQUEST messages, and may
        /// optionally be included in the DHCPACK and DHCPNAK messages.  DHCP
        /// servers include this option in the DHCPOFFER in order to allow the
        /// client to distinguish between lease offers.  DHCP clients use the
        /// contents of the 'server identifier' field as the destination address
        /// for any DHCP messages unicast to the DHCP server.  DHCP clients also
        /// indicate which of several lease offers is being accepted by including
        /// this option in a DHCPREQUEST message.
        /// 
        /// The identifier is the IP address of the selected server.
        /// 
        /// The code for this option is 54, and its length is 4.
        /// </summary>
        ServerIdentifier = 54,

        /// <summary>
        /// This option is used by a DHCP client to request values for specified
        /// configuration parameters.  The list of requested parameters is
        /// specified as n octets, where each octet is a valid DHCP option code
        /// as defined in this document.
        /// 
        /// The client MAY list the options in order of preference.  The DHCP
        /// server is not required to return the options in the requested order,
        /// but MUST try to insert the requested options in the order requested
        /// by the client.
        /// 
        /// The code for this option is 55.  Its minimum length is 1.
        /// </summary>
        ParameterRequestList = 55,

        /// <summary>
        /// This option is used by a DHCP server to provide an error message to a
        /// DHCP client in a DHCPNAK message in the event of a failure. A client
        /// may use this option in a DHCPDECLINE message to indicate the why the
        /// client declined the offered parameters.  The message consists of n
        /// octets of NVT ASCII text, which the client may display on an
        /// available output device.
        /// 
        /// The code for this option is 56 and its minimum length is 1.
        /// </summary>
        Message = 56,

        /// <summary>
        /// This option specifies the maximum length DHCP message that it is
        /// willing to accept.  The length is specified as an unsigned 16-bit
        /// integer.  A client may use the maximum DHCP message size option in
        /// DHCPDISCOVER or DHCPREQUEST messages, but should not use the option
        /// in DHCPDECLINE messages.
        ///
        /// The code for this option is 57, and its length is 2.  The minimum
        /// legal value is 576 octets.
        /// </summary>
        MaximumMessageSize = 57,

        /// <summary>
        /// Renewal (T1) Time Value
        /// 
        /// This option specifies the time interval from address assignment until
        /// the client transitions to the RENEWING state.
        /// 
        /// The value is in units of seconds, and is specified as a 32-bit
        /// unsigned integer.
        /// 
        /// The code for this option is 58, and its length is 4.
        /// </summary>
        /// <remarks>Recommended value is LeaseTime * 0.5</remarks>
        RenewalTime = 58,

        /// <summary>
        /// Rebinding (T2) Time Value
        /// 
        /// This option specifies the time interval from address assignment until
        /// the client transitions to the REBINDING state.
        /// 
        /// The value is in units of seconds, and is specified as a 32-bit
        /// unsigned integer.
        /// 
        /// The code for this option is 59, and its length is 4.
        /// </summary>
        /// <remarks>Recommended value is LeaseTime * 0.875</remarks>
        RebindingTime = 59,

        /// <summary>
        /// Vendor class identifier
        /// 
        /// This option is used by DHCP clients to optionally identify the vendor
        /// type and configuration of a DHCP client.  The information is a string
        /// of n octets, interpreted by servers.  Vendors may choose to define
        /// specific vendor class identifiers to convey particular configuration
        /// or other identification information about a client.  For example, the
        /// identifier may encode the client's hardware configuration.  Servers
        /// not equipped to interpret the class-specific information sent by a
        /// client MUST ignore it (although it may be reported). Servers that
        /// respond SHOULD only use option 43 to return the vendor-specific
        /// information to the client.
        /// 
        /// The code for this option is 60, and its minimum length is 1.
        /// </summary>
        ClassId = 60,

        /// <summary>
        /// Client-identifier
        /// 
        /// This option is used by DHCP clients to specify their unique
        /// identifier.  DHCP servers use this value to index their database of
        /// address bindings.  This value is expected to be unique for all
        /// clients in an administrative domain.
        /// 
        /// Identifiers SHOULD be treated as opaque objects by DHCP servers.
        /// 
        /// The client identifier MAY consist of type-value pairs similar to the
        /// 'htype'/'chaddr' fields defined in [3]. For instance, it MAY consist
        /// of a hardware type and hardware address. In this case the type field
        /// SHOULD be one of the ARP hardware types defined in STD2 [22].  A
        /// hardware type of 0 (zero) should be used when the value field
        /// contains an identifier other than a hardware address (e.g. a fully
        /// qualified domain name).
        /// 
        /// For correct identification of clients, each client's client-
        /// identifier MUST be unique among the client-identifiers used on the
        /// subnet to which the client is attached.  Vendors and system
        /// administrators are responsible for choosing client-identifiers that
        /// meet this requirement for uniqueness.
        /// 
        /// The code for this option is 61, and its minimum length is 2.
        /// </summary>
        ClientId = 61,

        /// <summary>
        /// This option specifies the name of the client's NIS+ [17] domain.  The
        /// domain is formatted as a character string consisting of characters
        /// from the NVT ASCII character set.
        /// 
        /// The code for this option is 64.  Its minimum length is 1.
        /// </summary>
        NisDomainName = 64,

        /// <summary>
        /// This option specifies a list of IP addresses indicating NIS+ servers
        /// available to the client.  Servers SHOULD be listed in order of
        /// preference.
        /// 
        /// The code for this option is 65.  Its minimum length is 4, and the
        /// length MUST be a multiple of 4.
        /// </summary>
        NisServer = 65,

        /// <summary>
        /// This option is used to identify a TFTP server when the 'sname' field
        /// in the DHCP header has been used for DHCP options.
        /// 
        /// The code for this option is 66, and its minimum length is 1.
        /// </summary>
        TftpServerName = 66,

        /// <summary>
        /// This option is used to identify a bootfile when the 'file' field in
        /// the DHCP header has been used for DHCP options.
        /// 
        /// The code for this option is 67, and its minimum length is 1.
        /// </summary>
        BootFileName = 67,

        /// <summary>
        /// This option specifies a list of IP addresses indicating mobile IP
        /// home agents available to the client.  Agents SHOULD be listed in
        /// order of preference.
        /// 
        /// The code for this option is 68.  Its minimum length is 0 (indicating
        /// no home agents are available) and the length MUST be a multiple of 4.
        /// It is expected that the usual length will be four octets, containing
        /// a single home agent's address.
        /// </summary>
        MobileIpHomeAgent = 68,

        /// <summary>
        /// The SMTP server option specifies a list of SMTP servers available to
        /// the client.  Servers SHOULD be listed in order of preference.
        /// 
        /// The code for the SMTP server option is 69.  The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        SmtpServer = 69,

        /// <summary>
        /// The POP3 server option specifies a list of POP3 available to the
        /// client.  Servers SHOULD be listed in order of preference.
        /// 
        /// The code for the POP3 server option is 70.  The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        Pop3Server = 70,

        /// <summary>
        /// The NNTP server option specifies a list of NNTP available to the
        /// client.  Servers SHOULD be listed in order of preference.
        /// 
        /// The code for the NNTP server option is 71. The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        NntpServer = 71,

        /// <summary>
        /// The WWW server option specifies a list of WWW available to the
        /// client.  Servers SHOULD be listed in order of preference.
        /// 
        /// The code for the WWW server option is 72.  The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        WwwServer = 72,

        /// <summary>
        /// The Finger server option specifies a list of Finger available to the
        /// client.  Servers SHOULD be listed in order of preference.
        /// 
        /// The code for the Finger server option is 73.  The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        FingerServer = 73,

        /// <summary>
        /// The IRC server option specifies a list of IRC available to the
        /// client.  Servers SHOULD be listed in order of preference.
        /// 
        /// The code for the IRC server option is 74.  The minimum length for
        /// this option is 4 octets, and the length MUST always be a multiple of
        /// 4.
        /// </summary>
        IrcServer = 74,

        /// <summary>
        /// The StreetTalk server option specifies a list of StreetTalk servers
        /// available to the client.  Servers SHOULD be listed in order of
        /// preference.
        ///
        /// The code for the StreetTalk server option is 75.  The minimum length
        /// for this option is 4 octets, and the length MUST always be a multiple
        /// of 4.
        /// </summary>
        StreetTalkServer = 75,

        /// <summary>
        /// The StreetTalk Directory Assistance (STDA) server option specifies a
        /// list of STDA servers available to the client.  Servers SHOULD be
        /// listed in order of preference.
        /// 
        /// The code for the StreetTalk Directory Assistance server option is 76.
        /// The minimum length for this option is 4 octets, and the length MUST
        /// always be a multiple of 4.
        /// </summary>
        StdaServer = 76,

        /// <summary>
        /// The URI for the captive portal API endpoint to which the user should connect (encoded following the rules in [RFC3986]).
        ///
        /// The code for the captive portal server option is 114.
        /// </summary>
        CaptivePortal = 114,

        /// <summary>
        /// The end option marks the end of valid information in the vendor
        /// field. Subsequent octets should be filled with pad options.
        /// 
        /// The code for the end option is 255, and its length is 1 octet.
        /// </summary>
        End = 255
    }
}
