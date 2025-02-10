using System.Threading;

namespace CCSWE.nanoFramework.Mediator.UnitTests.Mocks
{
    public interface IMediatorEventHandlerMock
    {
        public WaitHandle EventReceived { get; }
        public int EventsReceived { get; }
        public IMediatorEvent? LastEvent { get; }
    }

    public class MediatorEventHandlerMock: IMediatorEventHandler, IMediatorEventHandlerMock
    {
        private readonly AutoResetEvent _eventReceived = new(false);
        private int _eventsReceived;

        public WaitHandle EventReceived => _eventReceived;
        public int EventsReceived => _eventsReceived;
        public IMediatorEvent? LastEvent { get; private set; }

        public void HandleEvent(IMediatorEvent mediatorEvent)
        {
            LastEvent = mediatorEvent;

            Interlocked.Increment(ref _eventsReceived);

            _eventReceived.Set();
        }
    }
}
