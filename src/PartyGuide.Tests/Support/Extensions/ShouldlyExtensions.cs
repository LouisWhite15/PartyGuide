using Shouldly;
using System.Net;

namespace PartyGuide.Tests.Support.Extensions;

internal static class ShouldlyExtensions
{
    internal static void ShouldBeOk(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    internal static void ShouldBeBadRequest(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}
