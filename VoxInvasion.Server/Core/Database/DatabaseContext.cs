using Microsoft.EntityFrameworkCore;
using VoxInvasion.Server.Core.Database.Models;

namespace VoxInvasion.Server.Core.Database;

public class DatabaseContext : DbContext
{
    public DbSet<Player> Players { get; set; } = null!;

    public DatabaseContext() =>
        Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data Source=database.db");
}