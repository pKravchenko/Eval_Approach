using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace Eval_Approach.Tests;

[AttributeUsage(AttributeTargets.Method)]
public sealed class EvalTestAttribute : TestAttribute, IRepeatTest
{
    private const int RetryCount = 3;
    private static readonly IRepeatTest RetryImpl = new RetryAttribute(RetryCount);

    public TestCommand Wrap(TestCommand command) => RetryImpl.Wrap(command);
}
