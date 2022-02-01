using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHarness.Data;

public class Identity
{
    public Guid Id { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public string GivenName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
}

internal class IdentityConfigurator : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.Property(x => x.GivenName).IsRequired();
        builder.Property(x => x.Surname).IsRequired();
        builder.Property(x => x.Email).IsRequired();

        builder.HasData(SeedData.Identities);
    }
}