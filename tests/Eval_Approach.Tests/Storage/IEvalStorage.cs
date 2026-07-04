namespace Eval_Approach.Tests.Storage;

public interface IEvalStorage
{
    IEvalCache Cache { get; }
    IEvalReportSink Reports { get; }
}
