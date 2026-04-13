[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Threading.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Threading.TestFramework/) 

# CCSWE.nanoFramework.Threading.TestFramework

Helpers for managing `ThreadPool` state in nanoFramework unit tests. Because `ThreadPool` is a static singleton, tests that interact with it can leave state that affects subsequent tests. These utilities ensure the pool is reset to a known state before and after each test.

## API

### `ThreadPoolTestHelper`

```csharp
// Wrap a test body so the ThreadPool is reset after it runs (even on failure)
ThreadPoolTestHelper.ExecuteAndReset(() =>
{
    ThreadPool.QueueUserWorkItem(_ => { /* ... */ }, null);
    // assertions...
});
```

### `ThreadPoolManager`

```csharp
// Manually reset the ThreadPool between tests
ThreadPoolManager.Reset();
```

Use `ExecuteAndReset` when a single test needs cleanup. Use `ThreadPoolManager.Reset()` in a test setup/teardown method when multiple tests in a class share the pool.
