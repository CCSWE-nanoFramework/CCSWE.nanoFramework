[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Core.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Core/) 

# CCSWE.nanoFramework.Core

Shared utility classes used across the CCSWE.nanoFramework libraries.

## API

### `Ensure` / `ThrowHelper`

Argument validation helpers for use at public API boundaries:

- `Ensure.IsNotNull(paramName, value)` — throws `ArgumentNullException` if `value` is null
- `Ensure.IsNotNullOrEmpty(paramName, value)` — throws if the string is null or empty
- `ThrowHelper` — centralized exception throwing to keep call sites small

### `StringExtensions`

Extension methods on `string`:

- `Equals(string value, bool ignoreCase)` — case-insensitive equality comparison
- `Truncate(int maxLength)` — truncates to `maxLength` characters, appending `…` if truncated

### `Strings`

Static helpers for string operations:

- `Strings.EqualsIgnoreCase(string a, string b)` — null-safe case-insensitive comparison

### `Arrays`

Extension methods on `Array`:

- `ToArray(Array source, Type type)` — copies elements into a new typed array

### Reflection Extensions

- `MethodInfoExtensions` — helpers for working with `MethodInfo`
- `TypeExtensions` — helpers for working with `Type`
