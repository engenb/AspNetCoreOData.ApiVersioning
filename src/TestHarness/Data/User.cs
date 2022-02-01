using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHarness.Data;

public class User
{
    public Guid Id { get; set; }
        
    public Guid IdentityId { get; set; }
    public Identity Identity { get; set; }

    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
        
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<Role> Roles { get; set; } = new List<Role>();

    public bool Enabled { get; set; }
}

public class UserChangesConfigurator : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Identity)
            .WithMany(i => i.Users);

        builder
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>(
                ur2r => ur2r
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId),
                ur2u => ur2u
                    .HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId),
                ur => ur.HasKey(x => new {x.UserId, x.RoleId}));

        builder.HasData(SeedData.Users);
    }
}