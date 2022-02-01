using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHarness.Data;

public partial class Role
{
    public Guid Id { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<User> Users { get; set; } = new List<User>();

    public string Name { get; set; }
}

internal class RoleConfigurator : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name).IsRequired();

        builder.HasData(Role.All.Values);
    }
}