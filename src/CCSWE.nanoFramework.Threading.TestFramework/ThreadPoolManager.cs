using CCSWE.nanoFramework.Threading.Internal;

namespace CCSWE.nanoFramework.Threading.TestFramework
{
    /// <summary>
    /// A helper for managing ThreadPool during unit tests.
    /// </summary>
    /// <remarks>These methods are only intended for use during unit tests.</remarks>
    public static class ThreadPoolManager
    {
        /// <summary>
        /// Terminate existing thread pool threads and start from an initial state.
        /// </summary>
        public static void Reset()
        {
            var threadPool = ThreadPool.Instance;

            ThreadPool.Instance = new ThreadPoolInternal(ThreadPool.ThreadPoolWorkers, ThreadPool.ThreadPoolWorkItems);

            threadPool.Dispose();
        }

        /*
        /// <summary>
        /// Terminates existing thread pool threads.
        /// </summary>
        /// 
        public static void Stop()
        {
            ThreadPool.Instance.Dispose();
        }
        */
    }
}
