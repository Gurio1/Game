using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Game.Users.Data;

public class PlayerIdentityDbContext : IdentityDbContext
{
    public PlayerIdentityDbContext(DbContextOptions<PlayerIdentityDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<PlayerIdentity> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Users");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}