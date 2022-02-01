namespace TestHarness.Model.V3;

public class Organization
{
    public Guid Id { get; set; }

    public IEnumerable<User> Users { get; set; } = Array.Empty<User>();

    public string DisplayName { get; set; }
    public bool Enabled { get; set; }
    public bool SingleSignOnEnabled { get; set; }
}