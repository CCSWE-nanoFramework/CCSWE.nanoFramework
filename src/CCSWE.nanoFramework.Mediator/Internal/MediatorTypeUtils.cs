using System;

namespace CCSWE.nanoFramework.Mediator.Internal
{
    internal static class MediatorTypeUtils
    {
        internal static readonly Type MediatorEventType = typeof(IMediatorEvent);

        public static bool IsMediatorEvent(Type type)
        {
            return type.IsImplementationOf(MediatorEventType);
        }

        public static void RequireMediatorEvent(Type eventType)
        {
            if (!IsMediatorEvent(eventType))
            {
                throw new ArgumentException($"{eventType.Name} does not implement {nameof(IMediatorEvent)}");
            }
        }
    }
}
