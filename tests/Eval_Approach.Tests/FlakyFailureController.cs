namespace Eval_Approach.Tests;

internal static class FlakyFailureController
{
    private const string FailureCountEnvironmentVariable = "SIMULATED_FLAKY_FAILURE_COUNT";

    private static readonly Lazy<IReadOnlySet<string>> SelectedFailures = new(CreateSelectedFailures, true);

    public static void AssertTestPassesOrFails(string testName)
    {
        if (SelectedFailures.Value.Contains(testName))
        {
            Assert.Fail("Simulated flaky test failure");
        }
    }

    private static IReadOnlySet<string> CreateSelectedFailures()
    {
        var configuredFailureCount = ResolveFailureCount();
        var selectedTests = PlaceholderFlakyTests.AllTestNames
            .OrderBy(_ => Random.Shared.Next())
            .Take(configuredFailureCount)
            .ToHashSet(StringComparer.Ordinal);

        return selectedTests;
    }

    private static int ResolveFailureCount()
    {
        if (int.TryParse(Environment.GetEnvironmentVariable(FailureCountEnvironmentVariable), out var configuredCount))
        {
            return Math.Clamp(configuredCount, 0, Math.Min(3, PlaceholderFlakyTests.AllTestNames.Count));
        }

        return Random.Shared.NextDouble() < 0.2
            ? 0
            : Random.Shared.Next(1, 4);
    }
}
