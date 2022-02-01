using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHarness.Data;

public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}

public class UserRoleChangesConfigurator : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(SeedData.UsersRoles);
    }
}