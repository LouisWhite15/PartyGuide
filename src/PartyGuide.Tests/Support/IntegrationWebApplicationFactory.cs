using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PartyGuide.Persistence;

namespace PartyGuide.Tests.Support;

public class IntegrationWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextOptionsDescriptor = services.Single(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ApplicationDbContext>));

            var dbContextDescriptor = services.Single(
                d => d.ServiceType ==
                    typeof(ApplicationDbContext));

            services.Remove(dbContextOptionsDescriptor);
            services.Remove(dbContextDescriptor);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<IntegrationWebApplicationFactory>>();

            db.Database.EnsureCreated();
        });
    }
}
