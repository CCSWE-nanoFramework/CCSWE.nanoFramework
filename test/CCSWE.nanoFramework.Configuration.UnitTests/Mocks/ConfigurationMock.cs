using System;

namespace CCSWE.nanoFramework.Configuration.UnitTests.Mocks
{
    #pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    internal class ConfigurationMock
    {
        public static readonly ConfigurationMock Default = new(false, DateTime.MinValue, 4.321d, 1.234f, 4321, "Not A String", TimeSpan.MinValue);

        public const string Section = nameof(ConfigurationMock);
        public const string Section2 = nameof(ConfigurationMock) + "2";

        public ConfigurationMock()
        {

        }

        public ConfigurationMock(
            bool boolSetting,
            DateTime dateTimeSetting,
            double doubleSetting,
            float floatSetting,
            int intSetting,
            string stringSetting,
            TimeSpan timeSpanSetting)
        {
            BoolSetting = boolSetting;
            DateTimeSetting = dateTimeSetting;
            DoubleSetting = doubleSetting;
            FloatSetting = floatSetting;
            IntSetting = intSetting;
            StringSetting = stringSetting;
            TimeSpanSetting = timeSpanSetting;
        }

        public bool BoolSetting { get; set; }
        public DateTime DateTimeSetting { get; set; }
        public double DoubleSetting { get; set; }
        public float FloatSetting { get; set; }
        public int IntSetting { get; set; }
        public string StringSetting { get; set; }
        public TimeSpan TimeSpanSetting { get; set; }

        public static ConfigurationMock Create()
        {
            return new ConfigurationMock(true, DateTime.UtcNow, 0.1234d, 0.4321f, 1234, "A String", TimeSpan.FromMinutes(1234));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConfigurationMock);
        }

        public bool Equals(ConfigurationMock other)
        {
            if (other is null)
            {
                return false;
            }

            return BoolSetting == other.BoolSetting && 
                   DateTimeSetting == other.DateTimeSetting &&
                   Math.Abs(DoubleSetting - other.DoubleSetting) < double.Epsilon &&
                   Math.Abs(FloatSetting - other.FloatSetting) < float.Epsilon &&
                   IntSetting == other.IntSetting &&
                   StringSetting == other.StringSetting &&
                   TimeSpanSetting == other.TimeSpanSetting;
        }

        public override string ToString()
        {
            return $"{BoolSetting}|{DateTimeSetting}|{DoubleSetting}|{FloatSetting}|{IntSetting}|{StringSetting}|{TimeSpanSetting}";
        }
    }
    #pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
}
