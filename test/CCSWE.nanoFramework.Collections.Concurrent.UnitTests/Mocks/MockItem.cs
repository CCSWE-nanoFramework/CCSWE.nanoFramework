using System;

namespace CCSWE.nanoFramework.Collections.Concurrent.UnitTests.Mocks
{
    internal class MockItem
    {
        public Guid Id { get; } = Guid.NewGuid();

        public static bool operator ==(MockItem? left, MockItem? right) => left is not null && left.Equals(right);

        public static bool operator !=(MockItem? left, MockItem? right) => left is not null && !left.Equals(right);

        public override bool Equals(object? other)
        {
            return other is MockItem mockItem && Equals(mockItem);
        }

        public bool Equals(MockItem? other) => other is not null && Id.Equals(other.Id);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"{nameof(MockItem)}: {Id}";
    }
}
