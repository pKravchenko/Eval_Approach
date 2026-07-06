using System.Runtime.CompilerServices;

namespace Eval_Approach.Tests;

[TestFixture]
public class PlaceholderFlakyTests : EvalTestBase
{
    [EvalTest] public void Placeholder_01() => Run();
    [EvalTest] public void Placeholder_02() => Run();
    [EvalTest] public void Placeholder_03() => Run();
    [EvalTest] public void Placeholder_04() => Run();
    [EvalTest] public void Placeholder_05() => Run();
    [EvalTest] public void Placeholder_06() => Run();
    [EvalTest] public void Placeholder_07() => Run();
    [EvalTest] public void Placeholder_08() => Run();
    [EvalTest] public void Placeholder_09() => Run();
    [EvalTest] public void Placeholder_10() => Run();
    [EvalTest] public void Placeholder_11() => Run();
    [EvalTest] public void Placeholder_12() => Run();
    [EvalTest] public void Placeholder_13() => Run();
    [EvalTest] public void Placeholder_14() => Run();
    [EvalTest] public void Placeholder_15() => Run();
    [EvalTest] public void Placeholder_16() => Run();
    [EvalTest] public void Placeholder_17() => Run();
    [EvalTest] public void Placeholder_18() => Run();
    [EvalTest] public void Placeholder_19() => Run();
    [EvalTest] public void Placeholder_20() => Run();
    [EvalTest] public void Placeholder_21() => Run();
    [EvalTest] public void Placeholder_22() => Run();
    [EvalTest] public void Placeholder_23() => Run();
    [EvalTest] public void Placeholder_24() => Run();
    [EvalTest] public void Placeholder_25() => Run();
    [EvalTest] public void Placeholder_26() => Run();
    [EvalTest] public void Placeholder_27() => Run();
    [EvalTest] public void Placeholder_28() => Run();
    [EvalTest] public void Placeholder_29() => Run();
    [EvalTest] public void Placeholder_30() => Run();
    [EvalTest] public void Placeholder_31() => Run();
    [EvalTest] public void Placeholder_32() => Run();
    [EvalTest] public void Placeholder_33() => Run();
    [EvalTest] public void Placeholder_34() => Run();
    [EvalTest] public void Placeholder_35() => Run();
    [EvalTest] public void Placeholder_36() => Run();
    [EvalTest] public void Placeholder_37() => Run();
    [EvalTest] public void Placeholder_38() => Run();
    [EvalTest] public void Placeholder_39() => Run();
    [EvalTest] public void Placeholder_40() => Run();
    [EvalTest] public void Placeholder_41() => Run();
    [EvalTest] public void Placeholder_42() => Run();
    [EvalTest] public void Placeholder_43() => Run();
    [EvalTest] public void Placeholder_44() => Run();
    [EvalTest] public void Placeholder_45() => Run();
    [EvalTest] public void Placeholder_46() => Run();
    [EvalTest] public void Placeholder_47() => Run();
    [EvalTest] public void Placeholder_48() => Run();
    [EvalTest] public void Placeholder_49() => Run();
    [EvalTest] public void Placeholder_50() => Run();

    private void Run([CallerMemberName] string testName = "")
    {
        var passed = true;
        try
        {
            FlakyFailureController.AssertTestPassesOrFails(testName);
        }
        catch
        {
            passed = false;
            throw;
        }
        finally
        {
            Storage.Reports
                .WriteHtmlAsync(testName, BuildReport(testName, passed))
                .GetAwaiter().GetResult();
        }
    }

    private static string BuildReport(string testName, bool passed)
    {
        var score = passed ? Random.Shared.Next(3, 6) : Random.Shared.Next(1, 3);
        var rating = score >= 4 ? "good" : score >= 3 ? "acceptable" : "unacceptable";
        var failedStr = passed ? "false" : "true";
        var reason = passed ? "Simulated evaluation passed" : "Simulated flaky test failure";
        var diagnostics = passed
            ? ""
            : """{"severity":"error","message":"Simulated flaky test failure"}""";

        return $$"""
            {
              "scenarioName": "{{testName}}",
              "iterationName": "1",
              "creationTime": "{{DateTime.UtcNow:o}}",
              "evaluationResult": {
                "metrics": {
                  "Quality": {
                    "$type": "numeric",
                    "value": {{score}},
                    "name": "Quality",
                    "reason": "{{reason}}",
                    "interpretation": {
                      "rating": "{{rating}}",
                      "failed": {{failedStr}}
                    },
                    "diagnostics": [{{diagnostics}}]
                  }
                }
              }
            }
            """;
    }
}
