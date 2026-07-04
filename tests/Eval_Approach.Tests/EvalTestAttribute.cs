using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace Eval_Approach.Tests;

[AttributeUsage(AttributeTargets.Method)]
public sealed class EvalTestAttribute : TestAttribute, IRepeatTest
{
    private const int RetryCount = 3;

    public TestCommand Wrap(TestCommand command) => new RetryCommand(command, RetryCount);
}
