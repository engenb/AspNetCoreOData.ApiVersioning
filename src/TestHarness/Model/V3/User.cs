namespace TestHarness.Model.V3;

public class User
{
    public Guid Id { get; set; }

    public Guid IdentityId { get; set; }
    public Identity Identity { get; set; }

    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public IEnumerable<string> Roles { get; set; }
    
    public bool Enabled { get; set; }
}