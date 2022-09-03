using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PartyGuide.Persistence;

namespace PartyGuide.Tests.Support;

public abstract class FunctionalTestBase : IClassFixture<IntegrationWebApplicationFactory>, IDisposable
{
    private readonly IntegrationWebApplicationFactory _factory;

    protected abstract string Path { get; }

    protected ApplicationDbContext DbContext => _factory.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
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
        Client.Dispose();

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }
}
