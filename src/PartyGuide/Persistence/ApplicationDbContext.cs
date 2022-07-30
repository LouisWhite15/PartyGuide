using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PartyGuide.Configuration;
using PartyGuide.Contracts;
using PartyGuide.Persistence.Entities;

namespace PartyGuide.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly string _dbPath;

    public DbSet<GameEntity> Games { get; set; } = null!;

    public ApplicationDbContext(Config config)
    {
        _dbPath = config.ConnectionStrings.Sqlite;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlite($"Data Source={_dbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var valueComparer = new ValueComparer<List<Equipment>>(
            (c1, c2) => c1.SequenceEqual(c2),
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
