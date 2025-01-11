using System.IO;
using CCSWE.nanoFramework.Configuration.UnitTests.Mocks;
using nanoFramework.Json;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Configuration.UnitTests
{
    [TestClass]
    public class InternalConfigurationStorageTests
    {
        [Setup]
        public void Setup()
        {
            Assert.SkipTest("These tests can only be run on real hardware.");
        }

        [TestMethod]
        public void DeleteConfiguration_deletes_configuration_file()
        {
            var path = InternalConfigurationStorage.GetPath(ConfigurationMock.Section);
            var sut = new InternalConfigurationStorage();

            try
            {
                File.Create(path).Dispose();

                sut.DeleteConfiguration(ConfigurationMock.Section);

                Assert.IsFalse(File.Exists(path));
            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        [TestMethod]
        public void ReadConfiguration_returns_null_if_configuration_does_not_exist()
        {
            var sut = new InternalConfigurationStorage();

            Assert.IsNull(sut.ReadConfiguration(ConfigurationMock.Section, typeof(ConfigurationMock)), "Configuration is null");
        }

        [TestMethod]
        public void ReadConfiguration_throws_if_configuration_cannot_be_deserialized()
        {
            var path = InternalConfigurationStorage.GetPath(ConfigurationMock.Section);
            var sut = new InternalConfigurationStorage();

            try
            {
                File.WriteAllText(path, "Invalid JSON content");

                Assert.ThrowsException(typeof(DeserializationException), () =>
                {
                    sut.ReadConfiguration(ConfigurationMock.Section, typeof(ConfigurationMock));
                });
            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        [TestMethod]
        public void WriteConfiguration_can_be_read_by_ReadConfiguration()
        {
            var expected = ConfigurationMock.Create();
            var path = InternalConfigurationStorage.GetPath(ConfigurationMock.Section);
            var sut = new InternalConfigurationStorage();

            try
            {
                sut.WriteConfiguration(ConfigurationMock.Section, expected);
                Assert.IsTrue(File.Exists(path));

                var actual = sut.ReadConfiguration(ConfigurationMock.Section, typeof(ConfigurationMock));

                Assert.AreEqual(expected, actual);
            }
            finally
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
