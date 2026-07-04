namespace Eval_Approach.Tests.Storage;

public interface IEvalCache
{
    Task<string?> ReadAsync(string key, CancellationToken ct = default);
    Task WriteAsync(string key, string value, CancellationToken ct = default);
}
