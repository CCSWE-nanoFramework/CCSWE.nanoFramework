using System.Threading;

namespace CCSWE.nanoFramework.Mediator.Test.Shared
{
    public interface IMediatorSubscriberMock
    {
    }

    public class MediatorSubscriberMock: IMediatorSubscriber, IMediatorSubscriberMock
    {
        private int _eventsReceived;

        public int EventsReceived => _eventsReceived;
        public IMediatorEvent? LastEvent { get; private set; }

        public void HandleEvent(IMediatorEvent mediatorEvent)
        {
            LastEvent = mediatorEvent;

            Interlocked.Increment(ref _eventsReceived);
        }
    }
}
