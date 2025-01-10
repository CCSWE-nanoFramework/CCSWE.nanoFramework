using System;
using System.Collections;

namespace CCSWE.nanoFramework.Collections.Concurrent
{
    /// <summary>
    /// A thread-safe implementation of an <see cref="ArrayList"/>.
    /// </summary>
    /// <remarks>
    /// This is a brute force implementation using lock().
    /// Normally I would use a ReaderWriterLockSlim to optimize performance but this will work for now.
    /// </remarks>
    [Serializable]
    public class ConcurrentList : ICloneable, IList
    {
        private readonly ArrayList _list;
        private readonly object _lock = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentList"/> class that is empty, has the default initial capacity.
        /// </summary>
        public ConcurrentList() : this(new ArrayList())
        {

        }

        private ConcurrentList(ArrayList? arrayList)
        {
            _list = arrayList ?? new ArrayList();
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        public object this[int index]
        {
            get
            {
                lock (_lock)
                {
                    return _list[index];
                }
            }
            set
            {
                lock (_lock)
                {
                    _list[index] = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ConcurrentList"/>.
        /// </summary>
        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _list.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ConcurrentList"/> has a fixed size.
        /// </summary>
        /// <remarks>Always returns false.</remarks>
        public bool IsFixedSize => false;

        /// <summary>
        /// Gets a value indicating whether the <see cref="ConcurrentList"/> is read-only.
        /// </summary>
        /// <remarks>Always returns false.</remarks>
        public virtual bool IsReadOnly => false;

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="ConcurrentList"/> is synchronized (thread safe).
        /// </summary>
        /// <remarks>Always returns true.</remarks>
        public bool IsSynchronized => true;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="ConcurrentList"/>.
        /// </summary>
        /// <remarks>
        /// All calls are already synchronized so callers do not need to use this.
        /// </remarks>
        public object SyncRoot => this;

        /// <summary>
        /// Adds an object to the end of the <see cref="ConcurrentList"/>.
        /// </summary>
        /// <param name="value">The <see cref="object"/> to be added to the end of the <see cref="ArrayList"/>. The value can be <see langword="null"/>.</param>
        /// <returns>The index of the added item.</returns>
        public int Add(object? value)
        {
            lock (_lock)
            {
                return _list.Add(value);
            }
        }

        /// <summary>
        /// Removes all objects from the <see cref="ConcurrentList"/>.
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _list.Clear();
            }
        }

        /// <summary>
        /// Creates a shallow copy of the <see cref="ConcurrentList"/>.
        /// </summary>
        /// <returns>
        /// A shallow copy of the <see cref="ConcurrentList"/>.
        /// </returns>
        public object Clone()
        {
            lock (_lock)
            {
                return new ConcurrentList((ArrayList)_list.Clone());
            }
        }

        /// <summary>
        /// Determines whether an element is in the <see cref="ConcurrentList"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ConcurrentList"/>.</param>
        /// <returns>true if <paramref name="item"/> is found in the <see cref="ConcurrentList"/>; otherwise, false.</returns>
        public bool Contains(object? item)
        {
            lock (_lock)
            {
                return _list.Contains(item);
            }
        }

        /// <summary>
        /// Copies the <see cref="ConcurrentList"/> elements to an existing one-dimensional Array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ConcurrentList"/>.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Array array, int index)
        {
            lock (_lock)
            {
                ((ArrayList)_list.Clone()).CopyTo(array, index);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="ConcurrentList"/>.
        /// </summary>
        /// <returns>An IEnumerator for the <see cref="ConcurrentList"/>.</returns>
        public IEnumerator GetEnumerator()
        {
            lock (_lock)
            {
                return ((ArrayList)_list.Clone()).GetEnumerator();
            }
        }

        /// <summary>
        /// Searches for the specified <paramref name="value"/> and returns the zero-based index of the first occurrence within the entire <see cref="ConcurrentList"/>.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="ConcurrentList"/>. The value can be <see langword="null"/>.</param>
        /// <returns>The zero-based index of the first occurrence of value within the entire <see cref="ConcurrentList"/>, if found; otherwise, -1.</returns>
        public int IndexOf(object value)
        {
            lock (_lock)
            {
                return _list.IndexOf(value);
            }
        }

        /// <summary>
        /// Inserts an element into the <see cref="ConcurrentList"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value"/> should be inserted.</param>
        /// <param name="value">The <see cref="object"/> to insert. The `value` can be <see langword="null"/>.</param>
        public void Insert(int index, object value)
        {
            lock (_lock)
            {
                _list.Insert(index, value);
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ConcurrentList"/>.
        /// </summary>
        /// <param name="value">The <see cref="object"/> to remove from the <see cref="ConcurrentList"/>. The value can be <see langword="null"/>.</param>
        public void Remove(object value)
        {
            lock (_lock)
            {
                _list.Remove(value);
            }
        }

        /// <summary>
        /// Removes the element at the specified index of the <see cref="ConcurrentList"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            lock (_lock)
            {
                _list.RemoveAt(index);
            }
        }
    }
}
