using System;

namespace CCSWE.nanoFramework.Configuration.Internal
{
    internal class ConfigurationDescriptor
    {
        public ConfigurationDescriptor(string section, Type type, object defaults, IValidateConfiguration? validator = null)
        {
            if (defaults.GetType() != type)
            {
                throw new ArgumentException("Defaults is not the correct type", nameof(defaults));
            }

            Defaults = defaults;
            Section = NormalizeSection(section);
            Type = type;
            Validator = validator;
        }

        public object? Current { get; set; }

        public object Defaults { get; }

        public string Section { get; }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="ConfigurationDescriptor"/>.
        /// </summary>
        /// <value>
        /// An object that can be used to synchronize access to the <see cref="ConfigurationDescriptor"/>.
        /// </value>
        public object SyncRoot { get; } = new();

        public Type Type { get; }

        public IValidateConfiguration? Validator { get; }

        internal static string NormalizeSection(string name) => name.ToLower();
    }
}
