using System;
using System.Threading;

// ReSharper disable UnusedMember.Global
namespace CCSWE.nanoFramework.Threading
{
    /// <summary>
    /// Utility "extension" methods for <see cref="WaitHandle"/>.
    /// </summary>
    public static class WaitHandles
    {
        /// <summary>
        /// Waits for all the elements in the specified array to receive a signal, using an Int32 value to specify the time interval and specifying whether to exit the synchronization domain before the wait.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.</param>
        /// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false.</param>
        /// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object (duplicates).</param>
        /// <returns>true when every element in waitHandles has received a signal; otherwise, false.</returns>
        public static bool WaitAll(int millisecondsTimeout, bool exitContext, params WaitHandle[] waitHandles)
        {
            return WaitHandle.WaitAll(waitHandles, millisecondsTimeout, exitContext);
        }

        /// <summary>
        /// Waits for all the elements in the specified array to receive a signal.
        /// </summary>
        /// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object.</param>
        /// <returns>true when every element in waitHandles has received a signal; otherwise the method never returns.</returns>
        public static bool WaitAll(params WaitHandle[] waitHandles)
        {
            return WaitHandle.WaitAll(waitHandles);
        }

        /// <summary>
        /// Waits for all the elements in the specified array to receive a signal, using an Int32 value to specify the time interval and specifying whether to exit the synchronization domain before the wait.
        /// </summary>
        /// <param name="timeout">The amount of time to wait, or <see cref="Timeout.InfiniteTimeSpan"/> (-1) to wait indefinitely.</param>
        /// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false.</param>
        /// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object (duplicates).</param>
        /// <returns>true when every element in waitHandles has received a signal; otherwise, false.</returns>
        public static bool WaitAll(TimeSpan timeout, bool exitContext, params WaitHandle[] waitHandles)
        {
            return WaitAll((int)timeout.TotalMilliseconds, exitContext, waitHandles);
        }

        /// <summary>
        /// Waits for any of the elements in the specified array to receive a signal, using a 32-bit signed integer to specify the time interval, and specifying whether to exit the synchronization domain before the wait.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.</param>
        /// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false.</param>
        /// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait.</param>
        /// <returns>The array index of the object that satisfied the wait, or <see cref="WaitHandle.WaitTimeout"/> if no object satisfied the wait and a time interval equivalent to <paramref name="millisecondsTimeout"/> has passed.</returns>
        public static int WaitAny(int millisecondsTimeout, bool exitContext, params WaitHandle[] waitHandles)
        {
            return WaitHandle.WaitAny(waitHandles, millisecondsTimeout, exitContext);
        }

        /// <summary>
        /// Waits for any of the elements in the specified array to receive a signal.
        /// </summary>
        /// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait.</param>
        /// <returns>The array index of the object that satisfied the wait.</returns>
        public static int WaitAny(params WaitHandle[] waitHandles)
        {
            return WaitHandle.WaitAny(waitHandles);
        }

        /// <summary>
        /// Waits for any of the elements in the specified array to receive a signal, using a 32-bit signed integer to specify the time interval, and specifying whether to exit the synchronization domain before the wait.
        /// </summary>
        /// <param name="timeout">The amount of time to wait, or <see cref="Timeout.InfiniteTimeSpan"/> (-1) to wait indefinitely.</param>
        /// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false.</param>
        /// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait.</param>
        /// <returns>The array index of the object that satisfied the wait, or <see cref="WaitHandle.WaitTimeout"/> if no object satisfied the wait and a time interval equivalent to <paramref name="timeout"/> has passed.</returns>
        public static int WaitAny(TimeSpan timeout, bool exitContext, params WaitHandle[] waitHandles)
        {
            return WaitAny((int)timeout.TotalMilliseconds, exitContext, waitHandles);
        }

        /// <summary>
        /// Blocks the current thread until the current WaitHandle receives a signal, using a 32-bit signed integer to specify the time interval and specifying whether to exit the synchronization domain before the wait.
        /// </summary>
        /// <param name="waitHandle">The <see cref="WaitHandle"/> to wait on.</param>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="Timeout.Infinite"/> (-1) to wait indefinitely.</param>
        /// <returns>true if the current instance receives a signal; otherwise, false.</returns>
        public static bool WaitOne(this WaitHandle waitHandle, int millisecondsTimeout)
        {
            return waitHandle.WaitOne(millisecondsTimeout, false);
        }

        /// <summary>
        /// Blocks the current thread until the current WaitHandle receives a signal, using a 32-bit signed integer to specify the time interval and specifying whether to exit the synchronization domain before the wait.
        /// </summary>
        /// <param name="waitHandle">The <see cref="WaitHandle"/> to wait on.</param>
        /// <param name="timeout">The amount of time to wait, or <see cref="Timeout.InfiniteTimeSpan"/> (-1) to wait indefinitely.</param>
        /// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false.</param>
        /// <returns>true if the current instance receives a signal; otherwise, false.</returns>
        public static bool WaitOne(this WaitHandle waitHandle, TimeSpan timeout, bool exitContext = false)
        {
            return waitHandle.WaitOne((int)timeout.TotalMilliseconds, exitContext);
        }
    }
}
