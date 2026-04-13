[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.FileStorage.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.FileStorage/) 

# CCSWE.nanoFramework.FileStorage

An `IFileStorage` abstraction over the device file system, making storage access testable and swappable across different storage backends.

## API

### `IFileStorage`

| Method | Description |
|---|---|
| `Create(string path)` | Creates or overwrites a file and returns a `FileStream` |
| `Delete(string path)` | Deletes a file |
| `Exists(string path)` | Returns `true` if the file exists |
| `GetDirectories(string path)` | Lists subdirectories under the given path |
| `GetFiles(string path)` | Lists files in the given directory |
| `OpenRead(string path)` | Opens an existing file for reading |
| `OpenText(string path)` | Opens an existing file as a `StreamReader` |
| `OpenWrite(string path)` | Opens or creates a file for writing |
| `ReadAllBytes(string path)` | Reads the entire file as a `byte[]` |
| `ReadAllText(string path)` | Reads the entire file as a `string` |
| `WriteAllBytes(string path, byte[] bytes)` | Writes a `byte[]` to a file, replacing any existing content |
| `WriteAllText(string path, string text)` | Writes a `string` to a file, replacing any existing content |

### DI Registration

```csharp
services.AddFileStorage();
```
