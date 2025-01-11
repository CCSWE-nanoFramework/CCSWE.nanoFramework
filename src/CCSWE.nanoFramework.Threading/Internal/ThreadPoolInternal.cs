using System;
using System.Diagnostics;
using System.Threading;

namespace CCSWE.nanoFramework.Threading.Internal
{
    /// <summary>
    /// Provides a pool of threads that can be used to execute tasks, post work items, process asynchronous I/O, wait on behalf of other threads, and process timers.
    /// </summary>
    internal class ThreadPoolInternal: IDisposable
    {
        // TODO: Implement idle thread timeout?
        private const int ThreadPoolThreadTimeoutMs = 20 * 1000;

        private bool _disposed;
        private readonly object _lock = new();
        private WorkItemQueue _pendingWorkItems;
        private ThreadWorkerQueue _threadWorkers;

        internal CancellationToken CancellationToken { get; }

        private CancellationTokenSource CancellationTokenSource { get; } = new();

        /// <summary>
        ///  Gets the number of work items that are currently queued to be processed.
        /// </summary>
        /// <value>
        /// The number of work items that are currently queued to be processed.
        /// </value>
        // ReSharper disable once InconsistentlySynchronizedField
        public long PendingWorkItemCount => _pendingWorkItems.Count;

        /// <summary>
        /// Gets the number of thread pool threads that currently exist.
        /// </summary>
        /// <value>
        /// The number of thread pool threads that currently exist.
        /// </value>
        // ReSharper disable once InconsistentlySynchronizedField
        public int ThreadCount => _threadWorkers.Count;

        /// <summary>
        /// Maximum number of workers.
        /// </summary>
        public int Workers { get; }

        /// <summary>
        /// Maximum number of queued work items. Work is queued when all workers are already working.
        /// This allows for a maximum of (Workers+WorkItems) posted concurrently.
        /// </summary>
        public int WorkItems { get; }

        public ThreadPoolInternal(int workers, int workItems)
        {
            Ensure.IsInRange(workers > 0, $"{nameof(workers)} must be greater than zero", nameof(workers));
            Ensure.IsInRange(workItems > 0, $"{nameof(workItems)} must be greater than zero", nameof(workItems));

            CancellationToken = CancellationTokenSource.Token;
            Workers = workers;
            WorkItems = workItems;

            // Using fixed arrays for performance
            _pendingWorkItems = new WorkItemQueue(WorkItems);
            _threadWorkers = new ThreadWorkerQueue(Workers);
        }

        ~ThreadPoolInternal()
        {
            Dispose(false);
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

        internal void ExecutePendingWorkItems()
        {
            lock (_lock)
            {
                while (PendingWorkItemCount > 0)
                {
                    // Get an available worker
                    var worker = GetOrCreateFreeWorker();
                    if (worker is null)
                    {
                        // All workers are busy. The first to complete will resume this process.
                        break;
                    }

                    WorkItem work = default;

                    if (_pendingWorkItems.Dequeue(ref work))
                    {
                        worker.Post(work.CallBack, work.State);
                    }
                }
            }
        }

        private ThreadWorker? GetFreeWorker()
        {
            // NOTE: Creating outside the loop for efficiency
            // ReSharper disable once TooWideLocalVariableScope
            ThreadWorker worker;

            for (var i = 0; i < ThreadCount; i++)
            {
                worker = _threadWorkers[i];

                if (worker.IsFree)
                {
                    return worker;
                }
            }

            return null;
        }

        private ThreadWorker? GetOrCreateFreeWorker()
        {
            var worker = GetFreeWorker();
            if (worker is not null)
            {
                return worker;
            }

            if (ThreadCount >= Workers)
            {
                return null;
            }

            worker = new ThreadWorker(this);

            _threadWorkers.Enqueue(worker);

#if DEBUG
            Debug.WriteLine($"{_threadWorkers.Count} workers started");
#endif

            return worker;
        }

        /// <summary>
        /// Queues a method for execution, and specifies an object containing data to be used by the method. The method executes when a thread pool thread becomes available.
        /// </summary>
        /// <param name="callback">A <see cref="WaitCallback"/> representing the method to execute.</param>
        /// <param name="state">An object containing data to be used by the method.</param>
        /// <returns><see langword="true"/> if the method is successfully queued; <see langword="false"/> otherwise.</returns>
        public bool QueueUserWorkItem(WaitCallback callback, object? state = null)
        {
            ThrowIfDisposed();

            Ensure.IsNotNull(callback);

            lock (_lock)
            {
                var worker = GetOrCreateFreeWorker();
                if (worker is null)
                {
                    return _pendingWorkItems.Enqueue(new WorkItem(callback, state));
                }

                worker.Post(callback, state);
                return true;
            }
        }

        /// <summary>
        /// Sets the minimum number of active worker threads.
        /// </summary>
        /// <param name="workerThreads">The minimum number of active worker threads.</param>
        /// <returns><see langword="true"/> if the change is successful; otherwise, <see langword="false"/>.</returns>
        public bool SetMinThreads(int workerThreads) => SetMinThreads(workerThreads, false);

        internal bool SetMinThreads(int workerThreads, bool waitForCompletion)
        {
            ThrowIfDisposed();

            if (workerThreads <= 0 || workerThreads > Workers)
            {
                return false;
            }

            if (ThreadCount >= workerThreads)
            {
                return true;
            }

            var completionEvent = new ManualResetEvent(false);

            QueueUserWorkItem(_ =>
            {
                while (ThreadCount < workerThreads)
                {
                    QueueUserWorkItem(_ => { completionEvent.WaitOne(); });
                }

                completionEvent.Set();
            });

            if (waitForCompletion)
            {
                completionEvent.WaitOne();
            }

            return true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ThreadPoolInternal));
            }
        }
    }
}
