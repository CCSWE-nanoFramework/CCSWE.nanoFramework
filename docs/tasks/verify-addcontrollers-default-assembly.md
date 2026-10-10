# Verify AddControllers default assembly

## Context

`src/CCSWE.nanoFramework.WebServer/WebServer/Bootstrapper.cs:89` — `AddControllers()` with no assembly falls back to `Assembly.GetExecutingAssembly()`, which may resolve to the WebServer library rather than the app, so app controllers might not be discovered. Owner believes it works.

## Steps

1. On a device or emulator, run a sample app that calls `AddControllers()` without an assembly (e.g. uncomment `services.AddControllers();` in `samples/Samples.CCSWE.nanoFramework.WebServer/Program.cs`) and confirm its controllers are registered.
2. If they aren't, fix it (e.g. require the assembly or use the calling assembly) and update `src/CCSWE.nanoFramework.WebServer/README.md`.
3. Delete this file.
