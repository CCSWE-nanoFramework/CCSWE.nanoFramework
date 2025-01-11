using System;
using System.Threading;
using CCSWE.nanoFramework.Configuration.UnitTests.Mocks;
using CCSWE.nanoFramework.Threading.TestFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Configuration.UnitTests
{
    [TestClass]
    public class ConfigurationManagerTests
    {
        private ConfigurationManager CreateConfigurationManager(out ILogger logger, out ConfigurationStorageMock storage, bool validatesSuccessfully = true)
        {
            logger = new LoggerMock();
            storage = new ConfigurationStorageMock();

            var services = new ServiceCollection();
            services.BindConfiguration(ConfigurationMock.Section, ConfigurationMock.Default, new ConfigurationMockValidator(validatesSuccessfully));
            services.BindConfiguration(ConfigurationMock.Section2, ConfigurationMock.Default, new ConfigurationMockValidator(validatesSuccessfully));

            return new ConfigurationManager(logger, services.BuildServiceProvider(), storage);
        }

        [TestMethod]
        public void Clear_resets_configuration_section()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var expect = ConfigurationMock.Create();
                using var sut = CreateConfigurationManager(out _, out var storage);

                sut.Save(ConfigurationMock.Section, expect);
                var postSave = sut.Get(ConfigurationMock.Section);

                Assert.AreEqual(expect, postSave, "Saved correctly");

                sut.Clear(ConfigurationMock.Section);
                var postClear = sut.Get(ConfigurationMock.Section);

                Assert.AreEqual(ConfigurationMock.Default, postClear, "Cleared correctly");
                Assert.IsTrue(storage.DeleteConfigurationCalled, "Delete called");
                Assert.AreEqual(ConfigurationMock.Section.ToLower(), storage.DeleteConfigurationSection.ToLower(), "Delete section");
            });
        }

        [TestMethod]
        public void Clear_resets_configuration_sections()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var expect = ConfigurationMock.Create();
                using var sut = CreateConfigurationManager(out _, out var storage);

                sut.Save(ConfigurationMock.Section2, expect);
                var postSave = sut.Get(ConfigurationMock.Section2);

                Assert.AreEqual(expect, postSave, "Saved correctly");

                sut.Clear();
                var postClear = sut.Get(ConfigurationMock.Section2);

                Assert.AreEqual(ConfigurationMock.Default, postClear, "Cleared correctly");
                Assert.IsTrue(storage.DeleteConfigurationCalled, "Delete called");
                Assert.AreEqual(ConfigurationMock.Section2.ToLower(), storage.DeleteConfigurationSection.ToLower(), "Delete section");
            });
        }

        [TestMethod]
        public void Clear_throws_for_invalid_section()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Clear(null!); }, "Null section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Clear(string.Empty); }, "string.Empty section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Clear("Invalid Section"); }, "Invalid section");
            });
        }

        [TestMethod]
        public void Contains_returns_false()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.IsFalse(sut.Contains("InvalidSection"), "sut.Contains('InvalidSection')");
            });
        }

        [TestMethod]
        public void Contains_returns_true()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.IsTrue(sut.Contains(ConfigurationMock.Section), "sut.Contains(ConfigurationMock.Section)");
            });
        }

        [TestMethod]
        public void Get_returns_configuration()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var expect = ConfigurationMock.Default;
                using var sut = CreateConfigurationManager(out _, out var storage);

                var actual = sut.Get(ConfigurationMock.Section);

                Assert.AreEqual(expect, actual);
                Assert.IsTrue(storage.ReadConfigurationCalled, "Read called");
                Assert.AreEqual(ConfigurationMock.Section.ToLower(), storage.ReadConfigurationSection.ToLower(), "Read section");
                Assert.AreEqual(typeof(ConfigurationMock), storage.ReadConfigurationType, "Read type");
            });
        }

        [TestMethod]
        public void Get_throws_for_invalid_section()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Get(null!); }, "Null section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Get(string.Empty); }, "string.Empty section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Get("Invalid Section"); }, "Invalid section");
            });
        }

        [TestMethod]
        public void GetSections_returns_sections()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                var sections = sut.GetSections();

                Assert.AreEqual(2, sections.Length, "Sections count");
            });
        }

        [TestMethod]
        public void GetType_returns_type()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.AreEqual(typeof(ConfigurationMock), sut.GetType(ConfigurationMock.Section));
            });
        }

        [TestMethod]
        public void GetType_throws_for_invalid_section()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.GetType(null!); }, "Null section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.GetType(string.Empty); }, "string.Empty section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.GetType("Invalid Section"); }, "Invalid section");
            });
        }

        [TestMethod]
        public void Save_saves_configuration()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var expect = ConfigurationMock.Create();
                using var sut = CreateConfigurationManager(out _, out var storage);

                sut.Save(ConfigurationMock.Section, expect);
                var actual = sut.Get(ConfigurationMock.Section);

                Assert.AreEqual(expect, actual, "Saved correctly");
                Assert.IsTrue(storage.WriteConfigurationCalled, "Write called");
                Assert.AreEqual(storage.WriteConfigurationConfiguration, expect);
                Assert.AreEqual(ConfigurationMock.Section.ToLower(), storage.WriteConfigurationSection.ToLower(), "Write section");
            });
        }

        [TestMethod]
        public void Save_throws_for_invalid_section()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Save(null!, ConfigurationMock.Default); }, "Null section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Save(string.Empty, ConfigurationMock.Default); }, "string.Empty section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.Save("Invalid Section", ConfigurationMock.Default); }, "Invalid section");
            });
        }

        [TestMethod]
        public void Save_throws_for_null_configuration()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.ThrowsException(typeof(ArgumentNullException), () => { sut.Save(ConfigurationMock.Section, null!); });
            });
        }

        [TestMethod]
        public void Save_validates_configuration()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _, false);
                Assert.ThrowsException(typeof(ValidateConfigurationException), () => { sut.Save(ConfigurationMock.Section, ConfigurationMock.Default); });
            });
        }

        [TestMethod]
        public void SaveAsync_saves_configuration()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var expect = ConfigurationMock.Create();
                var saved = new ManualResetEvent(false);
                using var sut = CreateConfigurationManager(out _, out var storage);

                sut.ConfigurationChanged += (_, _) => { saved.Set(); };

                sut.SaveAsync(ConfigurationMock.Section, expect);

                saved.WaitOne();
                Thread.Sleep(100);

                var actual = sut.Get(ConfigurationMock.Section);

                Assert.AreEqual(expect, actual, "Saved correctly");
                Assert.IsTrue(storage.WriteConfigurationCalled, "Write called");
                Assert.AreEqual(storage.WriteConfigurationConfiguration, expect);
                Assert.AreEqual(ConfigurationMock.Section.ToLower(), storage.WriteConfigurationSection.ToLower(), "Write section");
            });
        }

        [TestMethod]
        public void SaveAsync_throws_for_invalid_section()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.SaveAsync(null!, ConfigurationMock.Default); }, "Null section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.SaveAsync(string.Empty, ConfigurationMock.Default); }, "string.Empty section");
                Assert.ThrowsException(typeof(ArgumentException), () => { sut.SaveAsync("Invalid Section", ConfigurationMock.Default); }, "Invalid section");
            });
        }

        [TestMethod]
        public void SaveAsync_throws_for_null_configuration()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _);
                Assert.ThrowsException(typeof(ArgumentNullException), () => { sut.SaveAsync(ConfigurationMock.Section, null!); });
            });
        }

        [TestMethod]
        public void SaveAsync_validates_configuration()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = CreateConfigurationManager(out _, out _, false);
                Assert.ThrowsException(typeof(ValidateConfigurationException), () => { sut.SaveAsync(ConfigurationMock.Section, ConfigurationMock.Default); });
            });
        }
    }
}
