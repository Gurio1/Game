using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Game.Users.Data;

public class UsersDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Users");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}