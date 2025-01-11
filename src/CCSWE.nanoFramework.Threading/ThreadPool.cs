using CCSWE.nanoFramework.Threading.Internal;

namespace CCSWE.nanoFramework.Threading
{
    /// <summary>
    /// Provides a pool of threads that can be used to execute tasks, post work items, process asynchronous I/O, wait on behalf of other threads, and process timers.
    /// </summary>
    public static class ThreadPool
    {
        internal const int ThreadPoolWorkers = 64;
        internal const int ThreadPoolWorkItems = 64;

        internal static ThreadPoolInternal Instance = new(ThreadPoolWorkers, ThreadPoolWorkItems);

        /// <summary>
        ///  Gets the number of work items that are currently queued to be processed.
        /// </summary>
        /// <value>
        /// The number of work items that are currently queued to be processed.
        /// </value>
        public static long PendingWorkItemCount => Instance.PendingWorkItemCount;

        /// <summary>
        /// Gets the number of thread pool threads that currently exist.
        /// </summary>
        /// <value>
        /// The number of thread pool threads that currently exist.
        /// </value>
        public static int ThreadCount => Instance.ThreadCount;

        /// <summary>
        /// Maximum number of workers.
        /// </summary>
        public static int Workers => Instance.Workers;

        /// <summary>
        /// Maximum number of queued work items. Work is queued when all workers are already working.
        /// This allows for a maximum of (Workers+WorkItems) posted concurrently.
        /// </summary>
        public static int WorkItems => Instance.WorkItems;

        /// <summary>
        /// Queues a method for execution, and specifies an object containing data to be used by the method. The method executes when a thread pool thread becomes available.
        /// </summary>
        /// <param name="callback">A <see cref="WaitCallback"/> representing the method to execute.</param>
        /// <param name="state">An object containing data to be used by the method.</param>
        /// <returns><see langword="true"/> if the method is successfully queued; <see langword="false"/> otherwise.</returns>
        public static bool QueueUserWorkItem(WaitCallback callback, object? state = null) => Instance.QueueUserWorkItem(callback, state);

        /// <summary>
        /// Sets the minimum number of active worker threads.
        /// </summary>
        /// <param name="workerThreads">The minimum number of active worker threads.</param>
        /// <returns><see langword="true"/> if the change is successful; otherwise, <see langword="false"/>.</returns>
        public static bool SetMinThreads(int workerThreads) => Instance.SetMinThreads(workerThreads);

        internal static bool SetMinThreads(int workerThreads, bool waitForCompletion) => Instance.SetMinThreads(workerThreads, waitForCompletion);
    }

    /// <summary>
    /// Represents a callback method to be executed by a thread pool thread.
    /// </summary>
    /// <param name="state">An object containing information to be used by the callback method.</param>
    public delegate void WaitCallback(object? state);
}
