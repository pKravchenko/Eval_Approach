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
        // Coherence: always passes, score 4-5
        var coherenceScore = Random.Shared.Next(4, 6);
        var coherenceRating = coherenceScore == 5 ? "exceptional" : "good";

        // Relevance: always passes, score 3-5
        var relevanceScore = Random.Shared.Next(3, 6);
        var relevanceRating = relevanceScore >= 4 ? "good" : "acceptable";

        // Quality: reflects overall pass/fail
        var qualityScore = passed ? Random.Shared.Next(3, 6) : Random.Shared.Next(1, 3);
        var qualityRating = qualityScore >= 4 ? "good" : qualityScore >= 3 ? "acceptable" : "unacceptable";
        var qualityFailed = !passed;
        var qualityReason = passed
            ? $"Score {qualityScore}/5: all checks passed"
            : $"Score {qualityScore}/5: {Random.Shared.Next(1, 5)} critical {Random.Shared.Next(3, 15)} errors";
        var qualityDiag = qualityFailed
            ? $$"""
              {"severity":"error","message":"Simulated flaky failure: judge score below threshold"},
              {"severity":"warning","message":"Low confidence: borderline score for {{testName}}"},
              {"severity":"informational","message":"Threshold used: 3/5"}
              """
            : """{"severity":"informational","message":"All quality checks passed"}""";

        return $$"""
            {
              "scenarioName": "{{testName}}",
              "iterationName": "1",
              "creationTime": "{{DateTime.UtcNow:o}}",
              "evaluationResult": {
                "metrics": {
                  "Coherence": {
                    "$type": "numeric",
                    "value": {{coherenceScore}},
                    "name": "Coherence",
                    "reason": "Response is coherent and logically structured (score {{coherenceScore}}/5)",
                    "interpretation": {
                      "rating": "{{coherenceRating}}",
                      "failed": false
                    },
                    "diagnostics": [
                      {"severity":"informational","message":"No coherence issues detected"}
                    ]
                  },
                  "Relevance": {
                    "$type": "numeric",
                    "value": {{relevanceScore}},
                    "name": "Relevance",
                    "reason": "Response addresses the prompt with score {{relevanceScore}}/5",
                    "interpretation": {
                      "rating": "{{relevanceRating}}",
                      "failed": false
                    },
                    "diagnostics": []
                  },
                  "Quality": {
                    "$type": "numeric",
                    "value": {{qualityScore}},
                    "name": "Quality",
                    "reason": "{{qualityReason}}",
                    "interpretation": {
                      "rating": "{{qualityRating}}",
                      "failed": {{(qualityFailed ? "true" : "false")}}
                    },
                    "diagnostics": [
                      {{qualityDiag}}
                    ]
                  }
                }
              }
            }
            """;
    }
}
