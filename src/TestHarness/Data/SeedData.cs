using Bogus;

namespace TestHarness.Data;

public static class SeedData
{
    public static IEnumerable<Organization> Organizations { get; }

    public static IEnumerable<Identity> Identities { get; }

    public static IEnumerable<User> Users { get; }
        
    public static IEnumerable<UserRole> UsersRoles { get; }
        
    static SeedData()
    {
        Randomizer.Seed = new Random(123456789);

        var faker = new Faker();

        Organizations = Enumerable.Range(0, 50)
            .Select(i => new Organization
            {
                Id = faker.Random.Guid(),
                DisplayName = faker.Company.CompanyName(),
                Enabled = true,
                SingleSignOnEnabled = faker.Random.Bool()
            })
            .ToArray();

        Identities = Enumerable.Range(0, 400)
            .Select(i =>
            {
                var givenName = faker.Name.FirstName();
                var surname = faker.Name.LastName();

                return new Identity
                {
                    Id = faker.Random.Guid(),
                    GivenName = givenName,
                    Surname = surname,
                    Email = faker.Internet.Email(givenName, surname)
                };
            })
            .ToArray();

        Users = Enumerable.Range(0, 500)
            .Select(i =>
            {
                var org = faker.PickRandom(Organizations);
                var identity = faker.PickRandom(Identities);
                return new User
                {
                    Id = faker.Random.Guid(),
                    IdentityId = identity.Id,
                    OrganizationId = org.Id,
                    Enabled = true
                };
            })
            .ToArray();

        UsersRoles = MapUserRoles(Users);
    }

    private static IEnumerable<UserRole> MapUserRoles(IEnumerable<User> users)
    {
        var faker = new Faker();

        var ret = new List<UserRole>();

        foreach (var user in users)
        {
            var profile = faker.Random.Int(0, 4);

            IEnumerable<string> userRoles;

            switch (profile)
            {
                case 0:
                {
                    userRoles = new List<string>
                    {
                        Role.AccountReadWrite,
                        Role.AssetTypeReadWrite,
                        Role.AuditRead,
                        Role.ClientReadWrite,
                        Role.CostCenterReadWrite,
                        Role.CustomFieldReadWrite,
                        Role.ExchangeRateReadWrite,
                        Role.FirmReadWrite,
                        Role.LeaseReadWrite,
                        Role.LessorReadWrite,
                        Role.PolicyReadWrite,
                        Role.ReportingEntityReadWrite,
                        Role.UserReadWrite
                    };
                    break;
                }
                case 1:
                {
                    userRoles = new List<string>
                    {      
                        Role.AccountReadWrite,
                        Role.AssetTypeReadWrite,
                        Role.AuditRead,
                        Role.ClientRead,
                        Role.CostCenterReadWrite,
                        Role.CustomFieldReadWrite,
                        Role.ExchangeRateReadWrite,
                        Role.FirmRead,
                        Role.LeaseReadWrite,
                        Role.LessorReadWrite,
                        Role.PolicyReadWrite,
                        Role.ReportingEntityReadWrite,
                        Role.UserRead,
                        Role.UserReadWriteClient,
                    };
                    break;
                }
                case 2:
                {
                    userRoles = new List<string>
                    {
                        Role.AccountReadWrite,
                        Role.AssetTypeReadWrite,
                        Role.AuditRead,
                        Role.ClientReadWrite,
                        Role.CostCenterReadWrite,
                        Role.CustomFieldReadWrite,
                        Role.ExchangeRateReadWrite,
                        Role.LeaseReadWrite,
                        Role.LessorReadWrite,
                        Role.PolicyReadWrite,
                        Role.ReportingEntityReadWrite,
                        Role.UserReadWrite
                    };
                    break;
                }
                case 3:
                {
                    userRoles = new List<string>
                    {
                        Role.AccountRead,
                        Role.AssetTypeRead,
                        Role.AuditRead,
                        Role.ClientRead,
                        Role.CostCenterRead,
                        Role.CustomFieldRead,
                        Role.ExchangeRateRead,
                        Role.LeaseReadWrite,
                        Role.LessorRead,
                        Role.PolicyRead,
                        Role.ReportingEntityRead,
                        Role.UserRead
                    };
                    break;
                }
                case 4:
                {
                    userRoles = new List<string>
                    {
                        Role.AccountRead,
                        Role.AssetTypeRead,
                        Role.AuditRead,
                        Role.ClientRead,
                        Role.CostCenterRead,
                        Role.CustomFieldRead,
                        Role.ExchangeRateRead,
                        Role.LeaseRead,
                        Role.LessorRead,
                        Role.PolicyRead,
                        Role.ReportingEntityRead,
                        Role.UserRead
                    };
                    break;
                }
                default: throw new Exception();
            }

            ret.AddRange(userRoles
                .Select(role => new UserRole
                {
                    UserId = user.Id,
                    RoleId = Role.ComputeId(role)
                }));
        }

        return ret.ToArray();
    }
}