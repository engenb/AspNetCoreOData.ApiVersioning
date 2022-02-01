namespace TestHarness.Model.V1;

public class User
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Enabled { get; set; }
}