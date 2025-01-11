namespace CCSWE.nanoFramework.DhcpServer.Options
{
    internal class UnknownOption: OptionBase
    {
        public UnknownOption(byte code, byte[] data): base(code, data)
        {
        }

        public UnknownOption(OptionCode code, byte[] data) : this((byte)code, data) { }

        public override string ToString() => ToString("??");
    }
}
