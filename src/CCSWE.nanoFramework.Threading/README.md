[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Threading.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Threading/) 

# CCSWE.nanoFramework.Threading

Utilities to simplify thread management on nanoFramework devices.

## `ThreadPool`

A general-purpose managed worker pool that eliminates the cost of spinning up new threads for each work item:

```csharp
ThreadPool.QueueUserWorkItem(state =>
{
    // work item runs on a pooled thread
}, null);
```

| Member | Description |
|---|---|
| `Workers` | Maximum number of worker threads (default: 64) |
| `WorkItems` | Maximum pending work item queue depth (default: 64) |
| `ThreadCount` | Current number of active threads |
| `PendingWorkItemCount` | Number of queued work items waiting to run |
| `QueueUserWorkItem(WaitCallback, object)` | Enqueue a work item |
| `SetMinThreads(int)` | Pre-warm a minimum number of threads |

## `ConsumerThreadPool`

A specialized pool that processes items from a queue using a fixed number of consumer threads:

```csharp
var pool = new ConsumerThreadPool(consumerCount: 2, (item) =>
{
    // process each item on a consumer thread
});

pool.Enqueue(myItem);

// dispose to stop consumer threads
pool.Dispose();
```

## `WaitHandles`

Helper methods for waiting on multiple `WaitHandle` instances:

```csharp
WaitHandles.WaitAll(new WaitHandle[] { handle1, handle2 });
WaitHandles.WaitAny(new WaitHandle[] { handle1, handle2 });
```
