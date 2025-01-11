namespace CCSWE.nanoFramework.Configuration.Internal
{
    internal class ConfigurationWorkItem
    {
        public object Configuration { get; }
        public ConfigurationDescriptor Descriptor { get; }

        public ConfigurationWorkItem(ConfigurationDescriptor descriptor, object configuration)
        {
            Configuration = configuration;
            Descriptor = descriptor;
        }
    }
}
