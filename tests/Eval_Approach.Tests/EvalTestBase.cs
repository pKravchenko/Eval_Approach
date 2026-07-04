using Eval_Approach.Tests.Storage;

namespace Eval_Approach.Tests;

public abstract class EvalTestBase
{
    // Shared across all tests and retries — storage is infrastructure, not test state.
    private static readonly Lazy<IEvalStorage> SharedStorage =
        new(EvalStorageFactory.Create, LazyThreadSafetyMode.ExecutionAndPublication);

    protected static IEvalStorage Storage => SharedStorage.Value;
}
