namespace Eval_Approach.Tests;

[TestFixture]
public class PlaceholderFlakyTests
{
    public static IReadOnlyList<string> AllTestNames { get; } =
        Enumerable.Range(1, 50)
            .Select(index => $"Placeholder_{index:00}")
            .ToArray();

    public static IEnumerable<TestCaseData> Cases =>
        AllTestNames.Select((testName, index) => new TestCaseData(index + 1).SetName(testName));

    [TestCaseSource(nameof(Cases))]
    public void Placeholder_test_cases_should_compile_and_run(int index)
    {
        FlakyFailureController.AssertTestPassesOrFails(TestContext.CurrentContext.Test.Name);

        Assert.That(index, Is.GreaterThan(0));
    }
}
