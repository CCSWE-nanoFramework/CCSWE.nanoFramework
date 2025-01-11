using System;

namespace CCSWE.nanoFramework.Mediator.UnitTests.Mocks
{
    public class MediatorEventMock: IMediatorEvent
    {
        public Guid Id { get; } = Guid.NewGuid();

        public override bool Equals(object other)
        {
            return other is MediatorEventMock otherEvent && Id.Equals(otherEvent.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(MediatorEventMock)}: {Id}";
        }
    }
}
