using System.Text;

namespace CCSWE.nanoFramework.DhcpServer.Options
{
    internal class ParameterRequestListOption : OptionBase
    {
        private string? _valueAsString;

        public ParameterRequestListOption(byte[] data): base(OptionCode.ParameterRequestList, data)
        {
        }

        public byte[] Deserialize()
        {
            return Data;
        }

        public static bool IsKnownOption(byte code) => IsKnownOption((OptionCode)code);

        public static bool IsKnownOption(OptionCode code) => OptionCode.ParameterRequestList == code;

        public override string ToString()
        {
            if (_valueAsString is null)
            {
                var stringBuilder = new StringBuilder(Length);
                var started = false;

                stringBuilder.Append('{');

                foreach (var b in Data)
                {
                    if (started)
                    {
                        stringBuilder.Append(',');
                    }

                    started = true;
                    stringBuilder.Append(b);
                }
                
                stringBuilder.Append('}');

                _valueAsString = stringBuilder.ToString();
            }

            return ToString(_valueAsString);
        }
    }
}
