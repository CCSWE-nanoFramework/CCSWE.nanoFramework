using System;
using System.Collections;
using CCSWE.nanoFramework.Configuration.Internal;
using CCSWE.nanoFramework.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Configuration
{
    internal class ConfigurationManager: IConfigurationManager, IDisposable
    {
        private bool _disposed;
        private readonly ConsumerThreadPool _configurationChangedThreadPool;
        private readonly Hashtable _configurationDescriptors = new ();
        private readonly object _lock = new();
        private readonly ILogger _logger;
        private readonly ConsumerThreadPool _saveConfigurationThreadPool;
        private readonly IConfigurationStorage _storage;

        public ConfigurationManager(ILogger logger, IServiceProvider serviceProvider, IConfigurationStorage storage)
        {
            _logger = logger;
            _storage = storage;

            var configurationDescriptors = serviceProvider.GetServices(typeof(ConfigurationDescriptor));

            foreach (var descriptor in configurationDescriptors)
            {
                if (descriptor is not ConfigurationDescriptor configurationDescriptor)
                {
                    continue;
                }

                AddDescriptor(configurationDescriptor);
            }

            _configurationChangedThreadPool = new ConsumerThreadPool(2, ConfigurationChangedThread);
            _saveConfigurationThreadPool = new ConsumerThreadPool(2, SaveConfigurationThread);
        }

        ~ConfigurationManager()
        {
            Dispose(false);
        }

        public event ConfigurationChangedEventHandler? ConfigurationChanged;

        private void AddDescriptor(ConfigurationDescriptor descriptor)
        {
            if (_configurationDescriptors.Contains(descriptor.Section))
            {
                throw new ArgumentException($"Configuration '{descriptor.Section}' has already been registered", nameof(descriptor));
            }

            _configurationDescriptors.Add(descriptor.Section, descriptor);
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ConfigurationManager));
            }
        }

        private void ConfigurationChangedThread(object item)
        {
            if (item is not ConfigurationWorkItem workItem)
            {
                return;
            }

            ConfigurationChanged?.Invoke(this, new ConfigurationChangedEventArgs(workItem.Descriptor.Section, workItem.Configuration));
        }

        public void Clear()
        {
            var sections = GetSections();

            foreach (var section in sections)
            {
                Clear(section);
            }
        }

        public void Clear(string section)
        {
            CheckDisposed();

            Ensure.IsNotNullOrEmpty(section);

            var descriptor = GetDescriptor(section);
            descriptor.Current = descriptor.Defaults;
            
            _configurationChangedThreadPool.Enqueue(new ConfigurationWorkItem(descriptor, descriptor.Current));

            // TODO: Do this async?
            // ReSharper disable once InconsistentlySynchronizedField
            _storage.DeleteConfiguration(descriptor.Section);
        }

        public bool Contains(string section)
        {
            return _configurationDescriptors[ConfigurationDescriptor.NormalizeSection(section)] is ConfigurationDescriptor;
        }

        private void Log(LogLevel logLevel, string message)
        {
            if (string.IsNullOrEmpty(message) || !_logger.IsEnabled(logLevel))
            {
                return;
            }

            _logger.Log(logLevel, $"[{nameof(ConfigurationManager)}] {message}");
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            lock (_lock)
            {
                if (_disposed)
                {
                    return;
                }

                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _configurationChangedThreadPool.Dispose();
            _saveConfigurationThreadPool.Dispose();

            if (disposing)
            {

            }

            _disposed = true;
        }


        private ConfigurationDescriptor GetDescriptor(string section)
        {
            return _configurationDescriptors[ConfigurationDescriptor.NormalizeSection(section)] as ConfigurationDescriptor ?? throw new ArgumentException($"Configuration '{section}' is not registered", nameof(section));
        }

        public object Get(string section)
        {
            CheckDisposed();

            Ensure.IsNotNullOrEmpty(section);

            var descriptor = GetDescriptor(section);
            var configuration = descriptor.Current;

            if (configuration is not null)
            {
                return configuration;
            }

            lock (descriptor.SyncRoot)
            {
                descriptor.Current = _storage.ReadConfiguration(descriptor.Section, descriptor.Type);

                return descriptor.Current ?? descriptor.Defaults;
            }
        }

        public string[] GetSections()
        {
            CheckDisposed();

            var configurationDescriptors = _configurationDescriptors.Values;
            var configurationSections = new string[configurationDescriptors.Count];
            var configurationSectionIndex = 0;
            
            foreach (var descriptor in configurationDescriptors)
            {
                if (descriptor is not ConfigurationDescriptor configurationDescriptor)
                {
                    throw new NullReferenceException();
                }

                configurationSections[configurationSectionIndex] = configurationDescriptor.Section;
                configurationSectionIndex++;
            }

            return configurationSections;
        }

        public Type GetType(string section)
        {
            CheckDisposed();

            Ensure.IsNotNullOrEmpty(section);

            var descriptor = GetDescriptor(section);

            return descriptor.Type;
        }

        public void Save(string section, object configuration)
        {
            CheckDisposed();

            Ensure.IsNotNullOrEmpty(section);
            Ensure.IsNotNull(configuration);

            var descriptor = GetDescriptor(section);

            ValidateConfiguration(descriptor, configuration);
            SaveConfigurationInternal(descriptor, configuration);
        }

        public void SaveAsync(string section, object configuration)
        {
            CheckDisposed();

            Ensure.IsNotNullOrEmpty(section);
            Ensure.IsNotNull(configuration);

            var descriptor = GetDescriptor(section);

            ValidateConfiguration(descriptor, configuration);

            Log(LogLevel.Trace, $"Queueing configuration save for {section}");

            _saveConfigurationThreadPool.Enqueue(new ConfigurationWorkItem(descriptor, configuration));
        }

        private void SaveConfigurationInternal(ConfigurationDescriptor descriptor, object configuration)
        {
            Log(LogLevel.Trace, $"Saving configuration {descriptor.Section}");

            lock (descriptor.SyncRoot)
            {
                descriptor.Current = configuration;

                _configurationChangedThreadPool.Enqueue(new ConfigurationWorkItem(descriptor, configuration));
                _storage.WriteConfiguration(descriptor.Section, configuration);
            }
        }

        private void SaveConfigurationThread(object item)
        {
            if (item is not ConfigurationWorkItem workItem)
            {
                return;
            }
                    
            SaveConfigurationInternal(workItem.Descriptor, workItem.Configuration);
        }

        private static void ValidateConfiguration(ConfigurationDescriptor descriptor, object configuration)
        {
            if (configuration.GetType() != descriptor.Type)
            {
                throw new ValidateConfigurationException(ValidateConfigurationResult.Fail("Configuration is not the correct type"));
            }

            var validator = descriptor.Validator;

            if (validator is null)
            {
                return;
            }

            var validationResults = validator.Validate(configuration);
            if (validationResults.Failed)
            {
                throw new ValidateConfigurationException(validationResults);
            }
        }
    }

    /// <summary>
    /// Encapsulates data related to a configuration change.
    /// </summary>
    public class ConfigurationChangedEventArgs
    {
        /// <summary>
        /// The configuration object.
        /// </summary>
        public object Configuration { get; }

        /// <summary>
        /// The section of the configuration.
        /// </summary>
        public string Section { get; }

        internal ConfigurationChangedEventArgs(string section, object configuration)
        {
            Configuration = configuration;
            Section = section;
        }
    }

    /// <summary>
    /// Fired when a configuration changes.
    /// </summary>
    public delegate void ConfigurationChangedEventHandler(object sender, ConfigurationChangedEventArgs e);
}
