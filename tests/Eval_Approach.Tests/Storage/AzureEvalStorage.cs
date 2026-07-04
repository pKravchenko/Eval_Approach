using Azure.Storage.Blobs;

namespace Eval_Approach.Tests.Storage;

public sealed class AzureEvalStorage : IEvalStorage
{
    public AzureEvalStorage(string connectionString, string containerName = "eval-results")
    {
        Cache = new AzureBlobEvalCache(connectionString, containerName, "cache");
        Reports = new AzureBlobEvalReportSink(connectionString, containerName, "reports");
    }

    public IEvalCache Cache { get; }
    public IEvalReportSink Reports { get; }
}

internal sealed class AzureBlobEvalCache : IEvalCache
{
    private readonly BlobContainerClient _container;
    private readonly string _prefix;

    public AzureBlobEvalCache(string connectionString, string containerName, string prefix)
    {
        _container = new BlobContainerClient(connectionString, containerName);
        _container.CreateIfNotExists();
        _prefix = prefix;
    }

    public async Task<string?> ReadAsync(string key, CancellationToken ct = default)
    {
        var blob = _container.GetBlobClient($"{_prefix}/{key}.json");
        if (!await blob.ExistsAsync(ct)) return null;
        var response = await blob.DownloadContentAsync(ct);
        return response.Value.Content.ToString();
    }

    public async Task WriteAsync(string key, string value, CancellationToken ct = default)
    {
        var blob = _container.GetBlobClient($"{_prefix}/{key}.json");
        await blob.UploadAsync(BinaryData.FromString(value), overwrite: true, cancellationToken: ct);
    }
}

internal sealed class AzureBlobEvalReportSink : IEvalReportSink
{
    private readonly BlobContainerClient _container;
    private readonly string _prefix;

    public AzureBlobEvalReportSink(string connectionString, string containerName, string prefix)
    {
        _container = new BlobContainerClient(connectionString, containerName);
        _container.CreateIfNotExists();
        _prefix = prefix;
    }

    public async Task WriteHtmlAsync(string testName, string html, CancellationToken ct = default)
    {
        var blob = _container.GetBlobClient($"{_prefix}/{testName}.html");
        await blob.UploadAsync(BinaryData.FromString(html), overwrite: true, cancellationToken: ct);
    }
}
