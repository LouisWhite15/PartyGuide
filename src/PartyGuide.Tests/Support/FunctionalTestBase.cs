using Microsoft.Extensions.DependencyInjection;
using PartyGuide.Persistence;

namespace PartyGuide.Tests.Support;

public abstract class FunctionalTestBase : IClassFixture<IntegrationWebApplicationFactory>, IDisposable
{
    private readonly IntegrationWebApplicationFactory _factory;

    protected abstract string Path { get; }
    
    protected HttpClient Client { get; }
    protected ApplicationDbContext DbContext { get; }

    public FunctionalTestBase(IntegrationWebApplicationFactory factory)
    {
        _factory = factory;

        Client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });

        DbContext = factory.Services.GetRequiredService<ApplicationDbContext>();
    }

    public void Dispose()
    {
    }
}
