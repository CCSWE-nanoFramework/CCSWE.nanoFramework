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
        private readonly Hashtable _subscriberTypes = new();

        /// <summary>
        /// Create a new instance of <see cref="AsyncMediator"/>
        /// </summary>
        /// <param name="options">The <see cref="AsyncMediatorOptions"/> used to configure this <see cref="AsyncMediator"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/> to log events to.</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> use to location singleton subscribers.</param>
        public AsyncMediator(AsyncMediatorOptions options, ILogger logger, IServiceProvider serviceProvider)
        {
            Ensure.IsNotNull(options);
            Ensure.IsNotNull(logger);
            Ensure.IsNotNull(serviceProvider);

            _logLevel = options.LogLevel;
            _logger = logger;
            _serviceProvider = serviceProvider;

            foreach (MediatorOptionsSubscriber subscriber in options.Subscribers)
            {
                Subscribe(subscriber.EventType, subscriber.SubscriberType);
            }

            _publishThreadPool = new ConsumerThreadPool(1, PublishThread);
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

            Ensure.IsNotNull(mediatorEvent);

            _publishThreadPool.Enqueue(mediatorEvent);
        }

        private void PublishInternal(IMediatorEvent mediatorEvent)
        {
            var eventName = mediatorEvent.GetType().FullName;

            if (_subscribers.Contains(eventName))
            {
                foreach (IMediatorEventHandler subscriber in (ArrayList)_subscribers[eventName])
                {
                    subscriber.HandleEvent(mediatorEvent);
                }
            }

            if (!_subscriberTypes.Contains(eventName))
            {
                return;
            }

            foreach (Type subscriberType in (ArrayList)_subscriberTypes[eventName])
            {
                var service = _serviceProvider.GetService(subscriberType);
                if (service is not IMediatorEventHandler subscriber)
                {
                    // Should I just log an error here instead?
                    throw new InvalidOperationException($"{service.GetType().FullName} registered as {subscriberType.FullName} does not implement {nameof(IMediatorEventHandler)}");
                }
                subscriber.HandleEvent(mediatorEvent);
            }
        }

        private void PublishThread(object state)
        {
            if (state is not IMediatorEvent mediatorEvent)
            {
                return;
            }

            PublishInternal(mediatorEvent);
        }

        /// <inheritdoc />
        public void Subscribe(Type eventType, IMediatorEventHandler eventHandler)
        {
            Ensure.IsNotNull(eventType);
            Ensure.IsNotNull(eventHandler);

            MediatorTypeUtils.RequireMediatorEvent(eventType);

            DebugLog($"Adding subscriber: {eventType.Name} - {eventHandler.GetType().Name}");
         
            var eventName = eventType.FullName;
            if (!_subscribers.Contains(eventName))
            {
                _subscribers.Add(eventName, new ArrayList { eventHandler });
                return;
            }

            var subscribers = (ArrayList)_subscribers[eventName];
            if (!subscribers.Contains(eventHandler))
            {
                subscribers.Add(eventHandler);
            }

        }

        /// <inheritdoc />
        public void Subscribe(Type eventType, Type subscriberType)
        {
            Ensure.IsNotNull(eventType);
            Ensure.IsNotNull(subscriberType);

            MediatorTypeUtils.RequireMediatorEvent(eventType);

            DebugLog($"Adding subscriber: {eventType.Name} - {subscriberType.Name}");

            var eventName = eventType.FullName;
            if (!_subscriberTypes.Contains(eventName))
            {
                _subscriberTypes.Add(eventName, new ArrayList { subscriberType });
                return;
            }

            var subscribers = (ArrayList)_subscriberTypes[eventName];
            if (!subscribers.Contains(subscriberType))
            {
                subscribers.Add(subscriberType);
            }
        }

        /// <inheritdoc />
        public void Unsubscribe(Type eventType, IMediatorEventHandler eventHandler)
        {
            Ensure.IsNotNull(eventType);
            Ensure.IsNotNull(eventHandler);

            DebugLog($"Removing subscriber: {eventType.Name} - {eventHandler.GetType().Name}");
         
            var eventName = eventType.FullName;
            if (!_subscribers.Contains(eventName))
            {
                return;
            }

            var subscribers = (ArrayList)_subscribers[eventName];
            if (subscribers.Contains(eventHandler))
            {
                subscribers.Remove(eventHandler);
            }
        }

        /// <inheritdoc />
        public void Unsubscribe(Type eventType, Type subscriberType)
        {
            Ensure.IsNotNull(eventType);
            Ensure.IsNotNull(subscriberType);

            DebugLog($"Removing subscriber: {eventType.Name} - {subscriberType.Name}");
          
            var eventName = eventType.FullName;
            if (!_subscriberTypes.Contains(eventName))
            {
                return;
            }

            var subscribers = (ArrayList)_subscriberTypes[eventName];
            if (subscribers.Contains(subscriberType))
            {
                subscribers.Remove(subscriberType);
            }
        }
    }
}
