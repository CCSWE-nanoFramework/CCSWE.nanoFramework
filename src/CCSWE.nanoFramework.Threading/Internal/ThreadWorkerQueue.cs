//
// Copyright (c) .NET Foundation and Contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.
//

using System;

namespace CCSWE.nanoFramework.Threading.Internal
{
    // TODO: Do I need synchronization here?
    // TODO: This doesn't need to be a queue but it does work for now...
    internal struct ThreadWorkerQueue
    {
        private readonly int _depth;
        private int _get;
        private int _put;
        private int _size;

        private readonly ThreadWorker[] _items;

        public ThreadWorkerQueue(int depth)
        {
            _depth = depth;
            _get = 0;
            _put = 0;
            _size = 0;

            _items = new ThreadWorker[depth];
        }

        public int Count => _size;
        public bool IsFull => Space == 0;
        public int Space => _depth - _size;

        public ThreadWorker this[int index]
        {
            get
            {
                ThreadWorker? item = default;

                if (Peek(ref item, index) && item is not null)
                {
                    return item;
                }

                throw new Exception("Invalid Index");
            }
        }

        public void Clear()
        {
            _get = 0;
            _put = 0;
            _size = 0;
        }

        public bool Dequeue(ref ThreadWorker item)
        {
            if (_size == 0)
            {
                return false;
            }

            item = _items[_get];

            _get++;
            _size--;

            if (_get >= _depth)
            {
                _get = 0;
            }

            return true;
        }

        public bool Enqueue(ThreadWorker item)
        {
            if (_size >= _depth)
            {
                return false;
            }

            _items[_put] = item;
            
            _put++;
            _size++;

            if (_put >= _depth)
            {
                _put = 0;
            }

            return true;
        }

        public bool Peek(ref ThreadWorker? item)
        {
            if (_size == 0)
            {
                return false;
            }

            item = _items[_get];
            
            return true;
        }

        public bool Peek(ref ThreadWorker? item, int index)
        {
            if (_size <= index)
            {
                return false;
            }

            if (index >= _depth)
            {
                index -= _depth;
            }

            item = _items[index];
            
            return true;
        }
    }
}
