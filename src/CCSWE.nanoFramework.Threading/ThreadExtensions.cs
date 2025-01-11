using System;
using System.Threading;

namespace CCSWE.nanoFramework.Threading
{
    /// <summary>
    /// Extension methods for <see cref="Thread"/>
    /// </summary>
    public static class ThreadExtensions
    {
        /// <summary>
        /// Calls <see cref="Thread.Join(int)"/> if not being called from the same thread.
        /// </summary>
        /// <param name="thread">The <see cref="Thread"/> to join.</param>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait for the thread to terminate.</param>
        /// <returns>true if the thread has terminated; false if the thread has not terminated after the amount of time specified by the millisecondsTimeout parameter has elapsed or called from the same thread.</returns>
        public static bool TryJoin(this Thread thread, int millisecondsTimeout)
        {
            try
            {
                if (Thread.CurrentThread != thread)
                {
                    return thread.Join(millisecondsTimeout);
                }
            }
            catch (Exception)
            {
                // Move along...
            }

            return false;
        }

        /// <summary>
        /// Calls <see cref="Thread.Join(TimeSpan)"/> if not being called from the same thread.
        /// </summary>
        /// <param name="thread">The <see cref="Thread"/> to join.</param>
        /// <param name="timeout">The amount of time to wait for the thread to terminate.</param>
        /// <returns>true if the thread has terminated; false if the thread has not terminated after the amount of time specified by the timeout parameter has elapsed or called from the same thread.</returns>
        public static bool TryJoin(this Thread thread, TimeSpan timeout)
        {
            return thread.TryJoin((int)timeout.TotalMilliseconds);
        }
    }
}
