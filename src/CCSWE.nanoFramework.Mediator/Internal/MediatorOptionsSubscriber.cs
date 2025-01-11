using System;

namespace CCSWE.nanoFramework.Mediator.Internal
{
    internal class MediatorOptionsSubscriber
    {
        internal Type EventType { get; }
        internal Type SubscriberType { get; }

        internal MediatorOptionsSubscriber(Type eventType, Type subscriberType)
        {
            EventType = eventType;
            SubscriberType = subscriberType;
        }
    }
}
