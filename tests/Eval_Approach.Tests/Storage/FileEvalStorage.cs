namespace Eval_Approach.Tests.Storage;

public sealed class FileEvalStorage : IEvalStorage
{
    public FileEvalStorage(string basePath)
    {
        Cache = new FileEvalCache(Path.Combine(basePath, "cache"));
        Reports = new FileEvalReportSink(Path.Combine(basePath, "reports"));
    }

    public IEvalCache Cache { get; }
    public IEvalReportSink Reports { get; }
}

internal sealed class FileEvalCache : IEvalCache
{
    private readonly string _directory;

    public FileEvalCache(string directory)
    {
        _directory = directory;
        Directory.CreateDirectory(directory);
    }

    public async Task<string?> ReadAsync(string key, CancellationToken ct = default)
    {
        var path = FilePath(key);
        return File.Exists(path) ? await File.ReadAllTextAsync(path, ct) : null;
    }

    public Task WriteAsync(string key, string value, CancellationToken ct = default) =>
        File.WriteAllTextAsync(FilePath(key), value, ct);

    private string FilePath(string key) => Path.Combine(_directory, $"{key}.json");
}

internal sealed class FileEvalReportSink : IEvalReportSink
{
    private readonly string _directory;

    public FileEvalReportSink(string directory)
    {
        _directory = directory;
        Directory.CreateDirectory(directory);
    }

    public Task WriteHtmlAsync(string testName, string html, CancellationToken ct = default) =>
        File.WriteAllTextAsync(Path.Combine(_directory, $"{testName}.html"), html, ct);
}
