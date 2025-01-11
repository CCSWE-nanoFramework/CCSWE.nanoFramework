using System;

namespace CCSWE.nanoFramework.Configuration.UnitTests.Mocks
{
    internal class ConfigurationStorageMock: IConfigurationStorage
    {
        public bool DeleteConfigurationCalled { get; set; }
        public string DeleteConfigurationSection { get; set; }
        
        public bool ReadConfigurationCalled { get; set; }
        public string ReadConfigurationSection { get; set; }
        public Type ReadConfigurationType { get; set; }
      
        public bool WriteConfigurationCalled { get; set; }
        public object WriteConfigurationConfiguration { get; set; }
        public string WriteConfigurationSection { get; set; }

        public void DeleteConfiguration(string section)
        {
            DeleteConfigurationCalled = true;
            DeleteConfigurationSection = section;
        }


        public object ReadConfiguration(string section, Type type)
        {
            ReadConfigurationCalled = true;
            ReadConfigurationSection = section;
            ReadConfigurationType = type;

            return null;
        }

        public void WriteConfiguration(string section, object configuration)
        {
            WriteConfigurationCalled = true;
            WriteConfigurationConfiguration = configuration;
            WriteConfigurationSection = section;
        }
    }
}
