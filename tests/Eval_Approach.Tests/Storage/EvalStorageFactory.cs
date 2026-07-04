namespace Eval_Approach.Tests.Storage;

internal static class EvalStorageFactory
{
    public static IEvalStorage Create()
    {
        var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        if (!string.IsNullOrEmpty(connectionString))
        {
            var container = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONTAINER") ?? "eval-results";
            return new AzureEvalStorage(connectionString, container);
        }

        var basePath = Environment.GetEnvironmentVariable("EVAL_STORAGE_PATH")
            ?? Path.Combine(Environment.CurrentDirectory, "eval-results");
        return new FileEvalStorage(basePath);
    }
}
