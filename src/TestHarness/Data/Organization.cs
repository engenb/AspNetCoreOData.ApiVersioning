using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestHarness.Data;

public class Organization
{
    public Guid Id { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public string DisplayName { get; set; }
    public bool Enabled { get; set; }
    public bool SingleSignOnEnabled { get; set; }
}

internal class OrganizationChangesConfigurator : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(x => x.DisplayName).IsRequired();

        builder.HasData(SeedData.Organizations);
    }
}