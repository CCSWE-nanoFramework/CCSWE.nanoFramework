namespace CCSWE.nanoFramework.Mediator
{
    /// <summary>
    /// An interface for subscribers of <see cref="IMediatorEvent"/>.
    /// </summary>
    public interface IMediatorEventHandler
    {
        /// <summary>
        /// Called by the <see cref="IMediator"/> when an <see cref="IMediatorEvent"/> is published.
        /// </summary>
        void HandleEvent(IMediatorEvent mediatorEvent);
    }
}
