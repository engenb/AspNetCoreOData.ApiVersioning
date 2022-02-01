using TestHarness.Model.V1;

namespace TestHarness.Model.V2;

public class User
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public string GivenName { get; set; }
    public string Surname { get; set; }
    public bool Enabled { get; set; }
}