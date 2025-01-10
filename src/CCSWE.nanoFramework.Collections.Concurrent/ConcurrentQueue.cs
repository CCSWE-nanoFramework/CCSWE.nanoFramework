using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
namespace CCSWE.nanoFramework.Collections.Concurrent
{
    // TODO: I might want to move most of this logic to a base class so creating strongly typed variants is trivial

    /// <summary>
    /// A thread-safe implementation of a <see cref="Queue"/>.
    /// </summary>
    /// <remarks>
    /// This is a brute force implementation using lock().
    /// Normally I would use a ReaderWriterLockSlim to optimize performance but this will work for now.
    /// </remarks>
    [Serializable]
    public class ConcurrentQueue : ICollection, ICloneable
    {
        private readonly object _lock = new();
        private readonly Queue _queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentQueue"/> class that is empty, has the default initial capacity, and uses the default growth factor (2x).
        /// </summary>
        public ConcurrentQueue(): this(new Queue())
        {

        }

        private ConcurrentQueue(Queue? queue)
        {
            _queue = queue ?? new Queue();
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ConcurrentQueue"/>.
        /// </summary>
        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _queue.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="ConcurrentQueue"/> is synchronized (thread safe).
        /// Always return true.
        /// </summary>
        public bool IsSynchronized => true;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="ConcurrentQueue"/>.
        /// </summary>
        /// <remarks>
        /// All calls are already synchronized so callers do not need to use this.
        /// </remarks>
        public object SyncRoot => this;

        /// <summary>
        /// Removes all objects from the <see cref="ConcurrentQueue"/>.
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _queue.Clear();
            }
        }

        /// <summary>
        /// Creates a shallow copy of the <see cref="ConcurrentQueue"/>.
        /// </summary>
        /// <returns>
        /// A shallow copy of the <see cref="ConcurrentQueue"/>.
        /// </returns>
        public object Clone()
        {
            lock (_lock)
            {
                return new ConcurrentQueue((Queue) _queue.Clone());
            }
        }

        /// <summary>
        /// Determines whether an element is in the <see cref="ConcurrentQueue"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ConcurrentQueue"/>.</param>
        /// <returns>true if <paramref name="item"/> is found in the <see cref="ConcurrentQueue"/>; otherwise, false.</returns>
        public bool Contains(object item)
        {
            if (item is null)
            {
                return false;
            }

            lock (_lock)
            {
                return _queue.Contains(item);
            }
        }

        /// <summary>
        /// Copies the <see cref="ConcurrentQueue"/> elements to an existing one-dimensional Array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ConcurrentQueue"/>.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Array array, int index)
        {
            lock (_lock)
            {
                ((Queue) _queue.Clone()).CopyTo(array, index);
            }
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the <see cref="ConcurrentQueue"/>.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the <see cref="ConcurrentQueue"/>.</returns>
        public object Dequeue()
        {
            lock (_lock)
            {
                return _queue.Dequeue();
            }
        }

        /// <summary>Adds an object to the end of the <see cref="ConcurrentQueue"/>.</summary>
        /// <param name="item">The object to add to the <see cref="ConcurrentQueue"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/> is <c>null</c>.</exception>
        public void Enqueue(object item)
        {
            Ensure.IsNotNull(item);

            lock (_lock)
            {
                _queue.Enqueue(item);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="ConcurrentQueue"/>.
        /// </summary>
        /// <returns>An IEnumerator for the <see cref="ConcurrentQueue"/>.</returns>
        public IEnumerator GetEnumerator()
        {
            lock (_lock)
            {
                return ((Queue)_queue.Clone()).GetEnumerator();
            }
        }

        /// <summary>
        /// Returns the object at the beginning of the <see cref="ConcurrentQueue"/> without removing it.
        /// </summary>
        /// <returns>The object at the beginning of the <see cref="ConcurrentQueue"/>.</returns>
        public object Peek()
        {
            lock (_lock)
            {
                return _queue.Peek();
            }
        }

        /// <summary>
        /// Copies the <see cref="ConcurrentQueue"/> elements to a new array. The order of the elements in the new
        /// array is the same as the order of the elements from the beginning of the <see cref="ConcurrentQueue"/>
        /// to its end.
        /// </summary>
        /// <returns>A new array containing elements copied from the <see cref="ConcurrentQueue"/>.</returns>
        public object[] ToArray()
        {
            lock (_lock)
            {
                return _queue.ToArray();
            }
        }

        /// <summary>
        /// Attempts to remove the object at the beginning of the <see cref="ConcurrentQueue"/>.
        /// </summary>
        /// <returns>true if an object was removed from the beginning of the <see cref="ConcurrentQueue"/>; otherwise false</returns>
        public bool TryDequeue([NotNullWhen(true)] out object? item)
        {
            lock (_lock)
            {
                item = _queue.Count > 0 ? _queue.Dequeue() : null;
            }

            return item is not null;
        }
    }
}
