using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PartyGuide.Contracts.Game;
using PartyGuide.Persistence.Entities;

namespace PartyGuide.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<GameEntity> Games { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var valueComparer = new ValueComparer<List<Equipment>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        modelBuilder
            .Entity<GameEntity>()
            .Property(e => e.RequiredEquipment)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(v => (Equipment)Enum.Parse(typeof(Equipment), v)).ToList() ?? new())
            .Metadata.SetValueComparer(valueComparer);
    }
}
