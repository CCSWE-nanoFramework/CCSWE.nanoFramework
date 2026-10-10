# Fix the failing CI build

The required checks fail on every PR (nf-tools dependency PRs since early September, and #107).

## Root cause

- **Solution / nanoFramework:** the test adapter updates nanoCLR to the latest on every run (`<CLRVersion>` is empty in `test/*/nano.runsettings`). nanoCLR v1.18 expects the `nanoFramework.System.IO.FileSystem` 1.1.96 native checksum; the repo pinned 1.1.94, so the Configuration and FileStorage test assemblies abort on load ("Invalid native checksum").
- **NuGets using latest version:** every nf-tools PR included `nanoFramework.Hardware.Esp32.Rmt` 2.x → 3.0.1 (required by `Iot.Device.Ws28xx.Esp32` 1.2.1040), which replaced `ClockDivider`/`SourceClockFrequency` with `ResolutionHz` and broke the NeoPixel build.

## Steps

1. Take all dependency updates and port NeoPixel to `ResolutionHz` (PR `fix/ci-build-deps`). Hardware-test NeoPixel on an ESP32 with current firmware.
2. Rebase and merge #107.
3. Re-enable the nf-tools workflow here and in `CoryCharlton/Emily.Clock`: `gh workflow enable update-dependencies.yml -R <repo>`.
4. Once the new CCSWE.nanoFramework packages are published, update Emily.Clock to them.
5. Mediator, Hosting and Logging tests load but report no results (skipped / zero passes) — predates this breakage; investigate separately.

Delete this file when done.
