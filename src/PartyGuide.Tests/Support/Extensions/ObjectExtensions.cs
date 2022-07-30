using ExpectedObjects;
using System.Diagnostics;

namespace PartyGuide.Tests.Support.Extensions;

internal static class ObjectExtensions
{
    [DebuggerStepThrough]
    internal static void ShouldBeLike(this object actual, object expected)
    {
        expected.ToExpectedObject().ShouldMatch(actual);
    }
}
