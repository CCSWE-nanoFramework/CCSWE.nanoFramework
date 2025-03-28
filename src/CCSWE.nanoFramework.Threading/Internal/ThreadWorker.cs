﻿//#define DEBUG_THREAD_WORKER

#if DEBUG_THREAD_WORKER
using System.Diagnostics;
#endif

using System;
using System.Threading;

namespace CCSWE.nanoFramework.Threading.Internal;

internal class ThreadWorker
{
    private WaitCallback? _callback;
    private readonly object _lock = new();
    private object? _state;
    private Thread? _thread;
    private readonly ThreadPoolInternal _threadPool;
    private readonly AutoResetEvent _workItemPosted = new(false);

    public ThreadWorker(ThreadPoolInternal threadPool)
    {
        _threadPool = threadPool;
    }

    // ReSharper disable once MergeConditionalExpression
    public int Id => _thread is not null ? _thread.ManagedThreadId : -1;

    public bool IsFree => _thread == null || _callback == null; // || _thread.ThreadState == ThreadState.Suspended;

    private void ExecuteWorkItems()
    {
#if DEBUG_THREAD_WORKER
        Debug.WriteLine($"Thread {Id} started");
#endif

        while (!_threadPool.CancellationToken.IsCancellationRequested)
        {
#if DEBUG_THREAD_WORKER
            Debug.WriteLine($"Thread {Id} started work");
#endif

            lock (_lock)
            {
                if (_callback is not null)
                {
                    _callback(_state);
                    _callback = null;
                    _state = null;
                }
            }

#if DEBUG_THREAD_WORKER
            Debug.WriteLine($"Thread {Id} completed work");
#endif

            _threadPool.ExecutePendingWorkItems();

            // If more work was posted to this worker as a result of call to ExecutePendingWorkItems, continue the work immediately
            if (_callback is not null)
            {
                continue;
            }

            if (_threadPool.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            var waitHandle = WaitHandles.WaitAny(_threadPool.CancellationToken.WaitHandle, _workItemPosted);
            if (waitHandle == 0)
            {
                break;
            }
        }

#if DEBUG_THREAD_WORKER
        Debug.WriteLine($"Thread {Id} completed");
#endif
    }

    public void Post(WaitCallback callback, object? state)
    {
        ArgumentNullException.ThrowIfNull(callback);

        lock (_lock)
        {
            _callback = callback;
            _state = state;

            Start();
        }
    }

    private void Start()
    {
        if (_thread is null)
        {
            _thread = new Thread(ExecuteWorkItems);
            _thread.Start();
        }
        else
        {
            _workItemPosted.Set();
        }
    }
}