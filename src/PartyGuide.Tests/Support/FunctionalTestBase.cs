using Microsoft.AspNetCore.Mvc.Testing;

namespace PartyGuide.Tests.Support;

public abstract class FunctionalTestBase : IClassFixture<IntegrationWebApplicationFactory>, IDisposable
{
    private readonly IntegrationWebApplicationFactory _factory;

    protected abstract string Path { get; }
    
    protected HttpClient Client { get; }

    public FunctionalTestBase(IntegrationWebApplicationFactory factory)
    {
        _factory = factory;

        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });
    }

    public void Dispose()
    {
    }
}
