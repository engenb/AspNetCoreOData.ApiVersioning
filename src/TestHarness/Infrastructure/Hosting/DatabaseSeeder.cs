using TestHarness.Data;

namespace TestHarness.Infrastructure.Hosting
{
    public class DatabaseSeeder : BackgroundService
    {
        private readonly IServiceProvider _services;

        public DatabaseSeeder(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            var sp = scope.ServiceProvider;

            var dbContext = sp.GetRequiredService<SampleDataDbContext>();

            dbContext.Roles.AddRange(Role.All.Values);
            dbContext.Organizations.AddRange(SeedData.Organizations);
            dbContext.Identities.AddRange(SeedData.Identities);
            dbContext.Users.AddRange(SeedData.Users);
            dbContext.UserRoles.AddRange(SeedData.UsersRoles);

            return dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}
