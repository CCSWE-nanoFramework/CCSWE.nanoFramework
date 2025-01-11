using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using CCSWE.nanoFramework.Logging;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.Hosting;

namespace CCSWE.nanoFramework.Hosting
{
    /// <summary>
    /// A device application initialization utility.
    /// </summary>
    public class DeviceHostBuilder: IHostBuilder
    {
        private IServiceProvider? _appServices;
        private readonly ArrayList _configureServicesActions;
        private HostBuilderContext? _hostBuilderContext;
        private bool _hostBuilt;
        private readonly ServiceProviderOptions _serviceProviderOptions;

        /// <summary>
        /// Initializes a new instance of <see cref="DeviceHostBuilder"/>.
        /// </summary>
        public DeviceHostBuilder()
        {
            _configureServicesActions = new ArrayList();

            if (Debugger.IsAttached)
            {
                // Set DI validation as default when debugger is attached   
                _serviceProviderOptions = new ServiceProviderOptions
                {
                    ValidateOnBuild = true
                };
            }
            else
            {
                _serviceProviderOptions = new ServiceProviderOptions();
            }
        }

        /// <inheritdoc />
        public object[] Properties { get; set; } = new object[0];

        // TODO: Look at copying this to nanoFramework.Hosting for improvements
        /// <inheritdoc />
        public IHost Build()
        {
            if (_hostBuilt)
            {
                throw new InvalidOperationException();
            }

            _hostBuilt = true;

            InitializeHostBuilderContext();
            InitializeServiceProvider();

            return ResolveHost(_appServices);
        }

        /// <inheritdoc />
        public IHostBuilder ConfigureServices(ServiceContextDelegate configureDelegate)
        {
            if (configureDelegate == null)
            {
                throw new ArgumentNullException();
            }

            _configureServicesActions.Add(configureDelegate);

            return this;
        }

        [MemberNotNull(nameof(_hostBuilderContext))]
        private void InitializeHostBuilderContext()
        {
            _hostBuilderContext = new HostBuilderContext(Properties);
        }

        [MemberNotNull(nameof(_appServices))]
        private void InitializeServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton(typeof(IHost), typeof(Internal.DeviceHost));
            services.AddSingleton(typeof(HostBuilderContext), _hostBuilderContext);

            foreach (ServiceContextDelegate configureServicesAction in _configureServicesActions)
            {
                configureServicesAction(_hostBuilderContext, services);
            }

            services.AddLogging();

            _appServices = services.BuildServiceProvider(_serviceProviderOptions);

            if (_appServices == null)
            {
                throw new InvalidOperationException();
            }
        }

        internal static IHost ResolveHost(IServiceProvider serviceProvider)
        {
            if (serviceProvider is null)
            {
                throw new InvalidOperationException();
            }

            return (IHost)serviceProvider.GetRequiredService(typeof(IHost));
        }

        // TODO: Not sure when this call is intended to be used but if it's before build then _hostBuilderContext will be null
        /// <inheritdoc />
        public IHostBuilder UseDefaultServiceProvider(ProviderContextDelegate configureDelegate)
        {
            if (configureDelegate == null)
            {
                throw new ArgumentNullException();
            }

            configureDelegate(_hostBuilderContext, _serviceProviderOptions);

            return this;
        }
    }
}
