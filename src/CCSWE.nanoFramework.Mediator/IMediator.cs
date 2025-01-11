using System;

namespace CCSWE.nanoFramework.Mediator
{
    /// <summary>
    /// A simple mediator pattern interface
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Publishes an event and calls each eventHandler.
        /// </summary>
        /// <param name="mediatorEvent">The event.</param>
        void Publish(IMediatorEvent mediatorEvent);

        /// <summary>
        /// Subscribes event handler to an event.
        /// </summary>
        /// <param name="eventType">Type of the event. The event must implement <see cref="IMediatorEvent"/>.</param>
        /// <param name="eventHandler">The eventHandler.</param>
        void Subscribe(Type eventType, IMediatorEventHandler eventHandler);

        /// <summary>
        /// Subscribes event handler to an event.
        /// </summary>
        /// <param name="eventType">Type of the event. The event must implement <see cref="IMediatorEvent"/>.</param>
        /// <param name="subscriberType">Type of the eventHandler (as registered in DI). The eventHandler must implement <see cref="IMediatorEventHandler"/>.</param>
        void Subscribe(Type eventType, Type subscriberType);

        /// <summary>
        /// Unsubscribes event handler from an event.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="eventHandler">The eventHandler.</param>
        public void Unsubscribe(Type eventType, IMediatorEventHandler eventHandler);

        /// <summary>
        /// Unsubscribes event handler from an event.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="subscriberType">Type of the eventHandler (as registered in DI).</param>
        void Unsubscribe(Type eventType, Type subscriberType);
    }
}
