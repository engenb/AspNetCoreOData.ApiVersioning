using System.Reflection;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace TestHarness.Data;

public class SampleDataDbContext : DbContext
{
    public virtual DbSet<Organization> Organizations { get; set; }
    public virtual DbSet<Identity> Identities { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<Role> Roles { get; set; }

    static SampleDataDbContext()
    {
        Randomizer.Seed = new Random(123456);
    }

    public SampleDataDbContext(DbContextOptions<SampleDataDbContext> options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}