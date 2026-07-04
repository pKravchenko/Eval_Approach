# Eval_Approach

This repository contains an NUnit-based .NET test suite with intentionally simulated flaky failures.

## Running the tests

```bash
dotnet test
```

The suite contains 50 placeholder tests. Most runs will fail 1-3 tests with the message `Simulated flaky test failure`, but some runs will pass with zero failures.
