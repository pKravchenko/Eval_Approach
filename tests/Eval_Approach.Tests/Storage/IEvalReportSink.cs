namespace Eval_Approach.Tests.Storage;

public interface IEvalReportSink
{
    Task WriteHtmlAsync(string testName, string html, CancellationToken ct = default);
}
