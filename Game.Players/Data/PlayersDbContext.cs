using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Game.Characters.Data;

public class PlayersDbContext : DbContext
{
    public PlayersDbContext(DbContextOptions<PlayersDbContext> options) : base(options)
    {
        
    }

    internal DbSet<Player> Players { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Players");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}