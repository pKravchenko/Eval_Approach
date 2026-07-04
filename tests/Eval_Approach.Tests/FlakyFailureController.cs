namespace Eval_Approach.Tests;

internal static class FlakyFailureController
{
    private const string ForcedFailureCountEnvironmentVariable = "SIMULATED_FLAKY_FAILURE_COUNT";
    private const double DefaultFailureProbability = 0.15;

    private static readonly IReadOnlyList<string> TestNames =
        Enumerable.Range(1, 50).Select(i => $"Placeholder_{i:00}").ToList();

    // Forced failures are stable within a run (env-var driven) — simulates a real regression.
    private static readonly Lazy<IReadOnlySet<string>> ForcedFailures =
        new(CreateForcedFailures, LazyThreadSafetyMode.ExecutionAndPublication);

    public static void AssertTestPassesOrFails(string testName)
    {
        if (ForcedFailures.Value.Contains(testName))
        {
            Assert.Fail("Simulated flaky test failure");
            return;
        }

        // Independent per-call random — simulates LLM judge noise.
        // With [EvalTest] retry(3), P(fail all 3) = 0.15³ ≈ 0.3%.
        if (Random.Shared.NextDouble() < DefaultFailureProbability)
            Assert.Fail("Simulated flaky test failure");
    }

    private static IReadOnlySet<string> CreateForcedFailures()
    {
        if (!int.TryParse(Environment.GetEnvironmentVariable(ForcedFailureCountEnvironmentVariable), out var count))
            return new HashSet<string>();

        count = Math.Clamp(count, 0, TestNames.Count);
        return TestNames
            .OrderBy(_ => Random.Shared.Next())
            .Take(count)
            .ToHashSet(StringComparer.Ordinal);
    }
}
