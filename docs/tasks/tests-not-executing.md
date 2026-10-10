# Tests that never execute

CI passes, but many tests never run. The adapter reports them as NotExecuted, and vstest doesn't count those.

- **No results at all:** Configuration, Hosting, Logging and Mediator. nanoCLR loads the assembly and prints `Ready. Done. Exiting.` with no test output.
- **Only some results:** Core, WebServer and several other projects.
- **When it started:** the Aug 2026 runs show it too (about 230 of 380 results NotExecuted in the TRX), so it predates the nanoCLR 1.18 update.
- **Where it sits:** likely in nanoFramework itself, in the test launcher or nanoCLR output capture. Upstream is moving test execution to dotnet tooling.

1. Set `<Logging>Verbose</Logging>` in one affected project's `nano.runsettings`; vstest-nanoframework v2 uses each project's own settings. Read the adapter output in CI.
2. Count TRX outcomes from the `vstest-results` artifact (`grep -o 'outcome="[A-Za-z]*"' *.trx | sort | uniq -c`) to confirm which tests ran.
3. Report upstream with a minimal repro, or fix it here if the cause is in this repo.

Delete this file when done.
