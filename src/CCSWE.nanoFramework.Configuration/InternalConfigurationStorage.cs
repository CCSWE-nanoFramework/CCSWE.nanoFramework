using System;
using System.IO;
using CCSWE.nanoFramework.Configuration.Internal;
using nanoFramework.Json;

namespace CCSWE.nanoFramework.Configuration
{
    internal class InternalConfigurationStorage: IConfigurationStorage
    {
        private const string Root = "I:";
        
        /// <summary>
        /// Gets the path to a configuration section.
        /// </summary>
        /// <param name="section">The configuration section.</param>
        /// <returns>An absolute path to the configuration section.</returns>
        /// <remarks>This is only visible for testing.</remarks>
        internal static string GetPath(string section) => Path.Combine(Root, $"ccswe-{ConfigurationDescriptor.NormalizeSection(section)}.config");

        /// <inheritdoc />
        public void DeleteConfiguration(string section)
        {
            var path = GetPath(section);

            if (!File.Exists(path))
            {
                return;
            }

            File.Delete(path);
        }

        /// <inheritdoc />
        public object? ReadConfiguration(string section, Type type)
        {
            var path = GetPath(section);

            if (!File.Exists(path))
            {
                return null;
            }

            using var stream = File.OpenRead(path);
            
            return JsonConvert.DeserializeObject(stream, type);
        }

        /// <inheritdoc />
        public void WriteConfiguration(string section, object configuration)
        {
            var path = GetPath(section);

            // TODO: Look into adding a JsonConvert method for serialization directly to a stream
            var serializedConfiguration = JsonConvert.SerializeObject(configuration);

            File.WriteAllText(path, serializedConfiguration);
        }
    }
}
