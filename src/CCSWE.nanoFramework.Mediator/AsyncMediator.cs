using System;
using System.Collections;
using CCSWE.nanoFramework.Mediator.Internal;
using CCSWE.nanoFramework.Threading;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.Mediator
{
    /// <summary>
    /// Threaded implementation of Mediator pattern
    /// </summary>
    public class AsyncMediator : IMediator, IDisposable
    {
        private bool _disposed;
        private readonly object _lock = new();
        private readonly ILogger _logger;
        private readonly LogLevel _logLevel;
        private readonly ConsumerThreadPool _publishThreadPool;
        private readonly IServiceProvider _serviceProvider;
        private readonly Hashtable _subscribers = new();

        /// <summary>
        /// Create a new instance of <see cref="AsyncMediator"/>
        /// </summary>
        /// <param name="options">The <see cref="AsyncMediatorOptions"/> used to configure this <see cref="AsyncMediator"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/> to log events to.</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> use to location singleton subscribers.</param>
        public AsyncMediator(AsyncMediatorOptions options, ILogger logger, IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(serviceProvider);

            _logLevel = options.LogLevel;
            _logger = logger;
            _serviceProvider = serviceProvider;

            foreach (MediatorEventHandlerDescriptor descriptor in options.Subscribers)
            {
                Subscribe(descriptor);
            }

            _publishThreadPool = new ConsumerThreadPool(1, PublishAsync);
        }

        /// <summary>
        /// Finalizes the <see cref="AsyncMediator"/>.
        /// </summary>
        ~AsyncMediator()
        {
            Dispose(false);
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(AsyncMediator));
            }
        }

        private void DebugLog(string message)
        {
            if (string.IsNullOrEmpty(message) || !_logger.IsEnabled(_logLevel))
            {
                return;
            }

            _logger.Log(_logLevel, $"[{nameof(AsyncMediator)}] {message}");
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

            if (disposing)
            {
                _publishThreadPool.Dispose();
            }

            _disposed = true;
        }

        /// <inheritdoc />
        public void Publish(IMediatorEvent mediatorEvent)
        {
            CheckDisposed();

            ArgumentNullException.ThrowIfNull(mediatorEvent);

            _publishThreadPool.Enqueue(mediatorEvent);
        }

        private void PublishAsync(object state)
        {
            if (state is not IMediatorEvent mediatorEvent)
            {
                return;
            }

            var eventName = mediatorEvent.GetType().FullName;

            if (!_subscribers.Contains(eventName))
            {
                return;
            }

            var subscribers = (ArrayList)_subscribers[eventName];

            foreach (MediatorEventHandlerDescriptor descriptor in subscribers)
            {
                descriptor.EventHandler?.HandleEvent(mediatorEvent);

                if (descriptor.ServiceType is null)
                {
                    continue;
                }

                var mediatorEventHandlerService = _serviceProvider.GetService(descriptor.ServiceType);
                if (mediatorEventHandlerService is not IMediatorEventHandler mediatorEventHandler)
                {
                    throw new InvalidOperationException($"{mediatorEventHandlerService.GetType().FullName} registered as {descriptor.ServiceType.FullName} does not implement {nameof(IMediatorEventHandler)}");
                }

                mediatorEventHandler.HandleEvent(mediatorEvent);
            }
        }

        private void Subscribe(MediatorEventHandlerDescriptor descriptor)
        {
            var eventName = descriptor.EventType.FullName;
            if (!_subscribers.Contains(eventName))
            {
                _subscribers.Add(eventName, new ArrayList { descriptor });
                return;
            }

            var descriptors = (ArrayList)_subscribers[eventName];

            if (!descriptors.Contains(descriptor))
            {
                descriptors.Add(descriptor);
            }
        }

        /// <inheritdoc />
        public void Subscribe(Type eventType, IMediatorEventHandler eventHandler)
        {
            ArgumentNullException.ThrowIfNull(eventType);
            ArgumentNullException.ThrowIfNull(eventHandler);

            MediatorTypeUtils.RequireMediatorEvent(eventType);

            DebugLog($"Adding subscriber: {eventType.Name} - {eventHandler.GetType().Name}");
         
            Subscribe(new MediatorEventHandlerDescriptor(eventType, eventHandler));
        }

        /// <inheritdoc />
        public void Subscribe(Type eventType, Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(eventType);
            ArgumentNullException.ThrowIfNull(serviceType);

            MediatorTypeUtils.RequireMediatorEvent(eventType);

            DebugLog($"Adding subscriber: {eventType.Name} - {serviceType.Name}");

            Subscribe(new MediatorEventHandlerDescriptor(eventType, serviceType));
        }

        /// <inheritdoc />
        public void Unsubscribe(Type eventType, IMediatorEventHandler eventHandler)
        {
            ArgumentNullException.ThrowIfNull(eventType);
            ArgumentNullException.ThrowIfNull(eventHandler);

            MediatorTypeUtils.RequireMediatorEvent(eventType);

            DebugLog($"Removing subscriber: {eventType.Name} - {eventHandler.GetType().Name}");
         
            var eventName = eventType.FullName;
            if (!_subscribers.Contains(eventName))
            {
                return;
            }

            var subscribers = (ArrayList)_subscribers[eventName];
            var subscriberCount = subscribers.Count;

            for (var i = subscriberCount - 1; i >= 0; i--)
            {
                var subscriber = (MediatorEventHandlerDescriptor) subscribers[i];
                if (subscriber.EventHandler == eventHandler)
                {
                    subscribers.RemoveAt(i);
                }
            }
        }

        /// <inheritdoc />
        public void Unsubscribe(Type eventType, Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(eventType);
            ArgumentNullException.ThrowIfNull(serviceType);

            MediatorTypeUtils.RequireMediatorEvent(eventType);

            DebugLog($"Removing subscriber: {eventType.Name} - {serviceType.Name}");
          
            var eventName = eventType.FullName;
            if (!_subscribers.Contains(eventName))
            {
                return;
            }

            var subscribers = (ArrayList)_subscribers[eventName];
            var subscriberCount = subscribers.Count;

            for (var i = subscriberCount - 1; i >= 0; i--)
            {
                var subscriber = (MediatorEventHandlerDescriptor)subscribers[i];
                if (subscriber.ServiceType == serviceType)
                {
                    subscribers.RemoveAt(i);
                }
            }
        }
    }
}
