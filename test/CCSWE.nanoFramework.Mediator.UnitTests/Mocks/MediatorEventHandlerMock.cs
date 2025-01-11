using System.Threading;

namespace CCSWE.nanoFramework.Mediator.UnitTests.Mocks
{
    public interface IMediatorEventHandlerMock
    {
    }

    public class MediatorEventHandlerMock: IMediatorEventHandler, IMediatorEventHandlerMock
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
