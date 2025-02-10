using System;
using System.Diagnostics.CodeAnalysis;

namespace CCSWE.nanoFramework.Mediator.Internal
{
    internal class MediatorEventHandlerDescriptor
    {
        /// <summary>
        /// Lazily initialized hash code
        /// </summary>
        private int _hashCode;

        internal MediatorEventHandlerDescriptor(Type eventType, IMediatorEventHandler eventHandler) : this(eventType, eventHandler, null)
        {
        }

        internal MediatorEventHandlerDescriptor(Type eventType, Type serviceType): this(eventType, null, serviceType)
        {
        }

        private MediatorEventHandlerDescriptor(Type eventType, IMediatorEventHandler? eventHandler, Type? serviceType)
        {
            EventType = eventType;
            EventHandler = eventHandler;
            ServiceType = serviceType;
        }

        public Type EventType { get; }

        public IMediatorEventHandler? EventHandler { get; set; }

        public Type? ServiceType { get; }

        public static bool operator ==(MediatorEventHandlerDescriptor? left, MediatorEventHandlerDescriptor? right) => left is not null && left.Equals(right);

        public static bool operator !=(MediatorEventHandlerDescriptor? left, MediatorEventHandlerDescriptor? right) => left is not null && !left.Equals(right);

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is MediatorEventHandlerDescriptor other && Equals(other);
        }

        public bool Equals([NotNullWhen(true)] MediatorEventHandlerDescriptor? other)
        {
            return other is not null && EventType == other.EventType && EventHandler == other.EventHandler && ServiceType == other.ServiceType;
        }

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            if (_hashCode == 0)
            {
                if (EventHandler is not null)
                {
                    _hashCode = EventType.GetHashCode() ^ EventHandler.GetHashCode();
                }
                else if (ServiceType is not null)
                {
                    _hashCode = EventType.GetHashCode() ^ ServiceType.GetHashCode();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            return _hashCode;
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }
    }
}
