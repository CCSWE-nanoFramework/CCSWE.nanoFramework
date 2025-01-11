using System;
using System.Threading;
using CCSWE.nanoFramework.Collections.Concurrent;

namespace CCSWE.nanoFramework.Threading
{
    /// <summary>
    /// Provides a specialized thread pool to process items from a queue.
    /// </summary>
    public class ConsumerThreadPool: IDisposable
    {
        private readonly ConsumerCallback _consumerCallback;
        private bool _disposed;
        private readonly AutoResetEvent _itemPending = new(false);
        private readonly ConcurrentQueue _items = new();
        private readonly object _lock = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerThreadPool"/> class specifying the number of consumer threads and the provided callback to process items.
        /// </summary>
        /// <param name="consumersThreads">The number of consumer threads to create.</param>
        /// <param name="consumerCallback">The callback responsible for processing items added.</param>
        public ConsumerThreadPool(int consumersThreads, ConsumerCallback consumerCallback)
        {
            Ensure.IsInRange(consumersThreads > 0, $"'{nameof(consumersThreads)}' must be greater than zero.", nameof(consumersThreads));
            Ensure.IsNotNull(consumerCallback);

            CancellationToken = CancellationTokenSource.Token;

            _consumerCallback = consumerCallback;

            var threadsStarting = ThreadPool.QueueUserWorkItem(_ => { CreateConsumerThreads(consumersThreads); });

            if (!threadsStarting)
            {
                // Thread pool was full, so we'll spin up a new thread to create the consumer threads
                new Thread(() => { CreateConsumerThreads(consumersThreads); }).Start();
            }
        }

        /// <summary>
        /// Finalizes the <see cref="ConsumerThreadPool"/>.
        /// </summary>
        ~ConsumerThreadPool()
        {
            Dispose(false);
        }

        private CancellationToken CancellationToken { get; }

        private CancellationTokenSource CancellationTokenSource { get; } = new();
 
        private void ConsumerThreadStart()
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                var waitHandle = WaitHandles.WaitAny(CancellationToken.WaitHandle, _itemPending);
                if (waitHandle == 0)
                {
                    break;
                }

                while (_items.TryDequeue(out var item))
                {
                    _consumerCallback(item);
                }
            }
        }

        private void CreateConsumerThreads(int consumersThreads)
        {
            for (var i = 0; i < consumersThreads; i++)
            {
                var consumerThread = new Thread(ConsumerThreadStart);
                consumerThread.Start();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            lock (_lock)
            {
                if (_disposed)
                {
                    return;
                }

                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            CancellationTokenSource.Cancel();

            if (disposing)
            {
                CancellationTokenSource.Dispose();
            }

            _disposed = true;
        }

        /// <summary>Adds an item to the end of the queue for processing.</summary>
        /// <param name="item">The object to add to the queue.</param>
        public void Enqueue(object item)
        {
            ThrowIfDisposed();

            Ensure.IsNotNull(item);

            _items.Enqueue(item);
            _itemPending.Set();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ConsumerThreadPool));
            }
        }
    }

    /// <summary>
    /// Represents a callback method to be executed by a <see cref="ConsumerThreadPool"/>.
    /// </summary>
    /// <param name="item">An object containing information to be used by the callback method.</param>
    public delegate void ConsumerCallback(object item);
}
