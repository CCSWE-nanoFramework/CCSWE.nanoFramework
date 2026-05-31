# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commit Guidelines

- Do not include `Co-Authored-By` or any AI attribution in commit messages.
- When staging for a commit, use `git add -A` but flag any changes that appear
  unrelated to the current task and ask whether to include them.
  
## What This Is

A collection of .NET nanoFramework libraries targeting embedded/IoT devices (ESP32, STM32, etc.). nanoFramework is a lightweight .NET implementation with significant constraints vs. full .NET — limited reflection, restricted async/await, manual thread management, and tight memory budgets.

## Build & Test Commands

nanoFramework does **not** use `dotnet` CLI. Use `msbuild` and `vstest.console.exe`.

```powershell
# Restore packages
nuget restore CCSWE.nanoFramework.sln

# Build the solution
msbuild CCSWE.nanoFramework.sln /p:Configuration=Release

# Run all unit tests (after build)
vstest.console.exe "test\*\bin\Release\NFUnitTest.dll" `
  /TestAdapterPath:"packages\nanoFramework.TestFramework.<version>\lib\net48\nanoFramework.TestAdapter.dll" `
  /Settings:"test\<ProjectName>.UnitTests\nano.runsettings"

# Run a single test project
vstest.console.exe "test\CCSWE.nanoFramework.Core.UnitTests\bin\Release\NFUnitTest.dll" `
  /TestAdapterPath:"packages\nanoFramework.TestFramework.<version>\lib\net48\nanoFramework.TestAdapter.dll" `
  /Settings:"test\CCSWE.nanoFramework.Core.UnitTests\nano.runsettings"

# Run a specific test class or method
vstest.console.exe "test\CCSWE.nanoFramework.Core.UnitTests\bin\Release\NFUnitTest.dll" `
  /TestAdapterPath:"packages\nanoFramework.TestFramework.<version>\lib\net48\nanoFramework.TestAdapter.dll" `
  /Settings:"test\CCSWE.nanoFramework.Core.UnitTests\nano.runsettings" `
  /Tests:"Namespace.ClassName.MethodName"
```

Test assemblies are compiled to `NFUnitTest.dll`. Each test project includes a `nano.runsettings` file with `IsRealHardware=false` (simulated device), 2-minute session timeout, and `net48` target framework.

CI/CD uses shared workflows from `CCSWE-nanoframework/actions-nanoframework` via GitHub Actions (see `.github/workflows/`).

### Building on Linux/Mono (local only)

nanoFramework officially builds on Windows/VS; CI uses Windows runners. A local Linux build works via a hybrid toolchain — Mono `msbuild` (its .NET Framework nano tasks, incl. the MetadataProcessor, load fine) driven with the .NET SDK's modern Roslyn (Mono's bundled Roslyn 3.9 only reaches C# 9; the source uses C# 10 file-scoped namespaces and C# 12 collection expressions). Three pieces are load-bearing:

1. **Targets path** — `.nfproj` imports `$(MSBuildExtensionsPath)\nanoFramework\v1.0\NFProjectSystem.*`. Under Mono, `MSBuildExtensionsPath` is the built-in default `/usr/lib/mono/xbuild` (not an env var). The nano targets ship inside the VS Code extension, so symlink them in (imports are `Exists`-guarded — without this they silently skip and the build is broken/empty rather than failing loudly):
   ```bash
   sudo ln -s ~/.vscode-server/extensions/nanoframework.vscode-nanoframework-<ver>/dist/utils/nanoFramework \
     /usr/lib/mono/xbuild/nanoFramework
   ```
2. **Modern compiler** — a wrapper at `~/.local/share/nf-build/csc` that execs the highest installed SDK's `Roslyn/bincore/csc.dll` via `dotnet`.
3. **`UseSharedCompilation=false`** — otherwise the Roslyn 3.9 VBCSCompiler server handles the compile and ignores the tool override.

Build command:
```bash
msbuild /p:Configuration=Release \
  /p:UseSharedCompilation=false \
  /p:CscToolPath=$HOME/.local/share/nf-build /p:CscToolExe=csc \
  src/<Project>/<Project>.nfproj
```

Re-point the symlink when the extension updates (path is version-pinned). This setup is local-only; it touches no project files and has no effect on Windows or CI builds.

## Repository Layout

```
src/    - Library source code (one folder per NuGet package)
test/   - Unit tests (mirrors src/ structure, .UnitTests suffix)
bench/  - Benchmark projects (nanoFramework.Benchmark)
samples/- Sample applications showing real device usage
```

## Architecture & Key Patterns

### Platform Constraints
- **No Task-based async** in core paths — uses manual `Thread` and `ThreadPool` management
- **Lock-based synchronization** instead of `ReaderWriterLockSlim` (not available in nanoFramework)
- **Limited reflection** — avoid patterns that depend on runtime type discovery
- **Memory-first design** — prefer stack allocation, avoid unnecessary allocations
- **No generics** — `Nullable<T>` (i.e., `int?`, `bool?`, etc.) is not available; use sentinel values (e.g., `0` or `-1`) or a separate boolean flag instead

### Hosting Model
`DeviceHost`/`DeviceHostBuilder` (in `CCSWE.nanoFramework.Hosting`) mirrors ASP.NET Core's `IHost`/`IHostBuilder`. Services are registered via extension methods and resolved through `IServiceProvider`. This is the entry point pattern for applications consuming these libraries.

### Dependency Injection
Uses `Microsoft.Extensions.DependencyInjection`. Each library exposes bootstrapper extension methods (e.g., `AddLogging()`, `AddConfigurationManager()`) that register services into `IServiceCollection`. Follow this pattern when adding new service registrations.

### Logging
`Microsoft.Extensions.Logging` interfaces (`ILogger`, `ILoggerFactory`) with a `ConsoleLogger` implementation in `CCSWE.nanoFramework.Logging`. Always inject `ILogger` rather than using static/global loggers.

### Web Server
`CCSWE.nanoFramework.WebServer` mimics ASP.NET Core: attribute routing (`[Route]`, `[HttpGet]`), `ControllerBase` inheritance, middleware pipeline (`IMiddleware`), built-in CORS and authentication. Request handling runs on a dedicated thread.

### Mediator / Events
`CCSWE.nanoFramework.Mediator` implements async pub/sub. Events implement `IMediatorEvent`; handlers implement `IMediatorEventHandler<T>`. The `AsyncMediator` uses a `ConsumerThreadPool` internally.

### Validation
`Ensure` class in `CCSWE.nanoFramework.Core` is the standard guard/precondition pattern used throughout. Use it for parameter validation at public API boundaries.

### Test Threading Helper
`CCSWE.nanoFramework.Threading.TestFramework` provides `ThreadPool` management utilities specifically for unit tests. Use it in test projects when the code under test interacts with `ThreadPool` (e.g., `ConsumerThreadPool`, `AsyncMediator`).

### Configuration
`IConfigurationManager` with pluggable `IConfigurationStorage` backends. Implementations validate via `IValidateConfiguration`. Storage can be in-memory (`InternalConfigurationStorage`) or backed by the file system via `CCSWE.nanoFramework.FileStorage`.

## Versioning & Packaging

- **Nerdbank.GitVersioning** drives version numbers from `version.json` — do not manually set version properties in `.nfproj` files
- Each library has a corresponding `.nuspec` for NuGet packaging
- Packages publish from `master` and `v\d+.\d+` branches per `version.json` `publicReleaseRefSpec`
- Keep `.nuspec` dependency versions in sync with `packages.config` — whenever a package version is updated in `packages.config`, the corresponding `.nuspec` `<dependency>` must be updated to match

## Project File Format

Projects use the nanoFramework project type GUID `{11A8DD76-328B-46DF-9F39-F559912D0360}` and `.nfproj` extension. These are MSBuild projects requiring the nanoFramework MSBuild extension. Do not convert them to SDK-style `.csproj`.

## Code Style

Within each C# file, order members as: fields, constructors, properties, methods — with each group alphabetized.

Use `is null` / `is not null` instead of `== null` / `!= null` for null checks.

Always use curly braces for control flow (`if`, `for`, `foreach`, `while`, etc.) — never omit them for single-line bodies.

Use Allman brace style (opening brace on its own line) for all blocks: methods, classes, `if`/`else`, loops, `switch`, etc. Exception: single-line expression-bodied members (`=>`) are fine for trivial getters or one-liner methods.

Use `switch` expressions (`x switch { ... }`) instead of `switch` statements where the intent is to return or assign a value.

Use file-scoped namespaces (`namespace Foo.Bar;`) rather than block-scoped namespaces (`namespace Foo.Bar { ... }`).

All public API members (classes, methods, properties, fields, enums and their values) must have XML documentation comments (`<summary>`, `<param>`, `<returns>`, `<exception>` as applicable).

## Unit Tests

Uses `nanoFramework.TestFramework`. Follow these conventions:

- Use `Assert.AreEqual` not `Assert.Equal` (the latter is obsolete)
- Do not add "Arrange / Act / Assert" comments in tests
