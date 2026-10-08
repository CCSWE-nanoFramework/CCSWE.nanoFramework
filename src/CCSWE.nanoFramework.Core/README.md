[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/blob/master/LICENSE.md) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Core.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Core/) 

# CCSWE.nanoFramework.Core

Shared utility classes used across the CCSWE.nanoFramework libraries.

## API

### `Ensure` / `ThrowHelper`

Argument validation helpers for use at public API boundaries:

- `Ensure.IsValid(expression)` — throws `ArgumentException` if `expression` is `false`
- `Ensure.IsInRange(value, min, max)` — throws `ArgumentOutOfRangeException` if `value` is outside `min`..`max`
- `Ensure.IsNotNull(value)` / `Ensure.IsNotNullOrEmpty(value)` — obsolete; use `ArgumentNullException.ThrowIfNull` / `ArgumentException.ThrowIfNullOrEmpty`
- `ThrowHelper` — obsolete; use `ArgumentNullException.ThrowIfNull` / `ArgumentException.ThrowIfNullOrEmpty`

### `StringExtensions`

Extension methods on `string`:

- `Equals(string? other, bool ignoreCase)` — equality comparison, optionally case-insensitive
- `Truncate(int maxLength)` — truncates to `maxLength` characters, ending in `...` if truncated

### `Strings`

Static helpers for string operations:

- `Strings.EqualsIgnoreCase(string a, string b)` — null-safe case-insensitive comparison

### `Arrays`

Extension methods on `Array`:

- `ToArray(Array source, Type type)` — copies elements into a new typed array

### Reflection Extensions

- `MethodInfoExtensions` — helpers for working with `MethodInfo`
- `TypeExtensions` — helpers for working with `Type`
