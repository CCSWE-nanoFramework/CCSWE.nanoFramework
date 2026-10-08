# Fix the failing CI build

The required checks fail on every PR (nf-tools dependency PRs since early September, and #107):

- **NuGets using latest version:** `nuget update` finds newer packages (e.g. `nanoFramework.System.Net`, `nanoFramework.Hardware.Esp32.Rmt`). The open nf-tools PRs that would update them can't merge while the build fails.
- **Solution / nanoFramework:** the nanoCLR test runner reports "Firmware version does not match managed code version"; `AddConfigurationManager_should_register_ConfigurationManager` and `AddFileStorage_should_register_FileStorage` fail, and the Mediator tests are skipped.

1. Find why the test runner's nanoCLR doesn't match the referenced `mscorlib` (nanoclr update step in `actions-nanoframework`/`vstest-nanoframework`, or package versions).
2. Get the build green, then merge the nf-tools PRs and #107 (Mediator fix; its new tests haven't run yet).

Delete this file when done.
