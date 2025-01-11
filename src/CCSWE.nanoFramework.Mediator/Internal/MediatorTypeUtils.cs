using System;

namespace CCSWE.nanoFramework.Mediator.Internal
{
    internal static class MediatorTypeUtils
    {
        public static bool IsMediatorEvent(Type type)
        {
            var interfaces = type.GetInterfaces();
            foreach (var current in interfaces)
            {
                if (current == typeof(IMediatorEvent))
                {
                    return true;
                }
            }

            return false;
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
