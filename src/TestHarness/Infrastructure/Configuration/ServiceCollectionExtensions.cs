namespace TestHarness.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServiceOptions(this IServiceCollection services) =>
        services
            .ConfigureOptions<ApiExplorerConfigurator>()
            .ConfigureOptions<ApiVersioningConfigurator>()
            .ConfigureOptions<ODataConfigurator>()
            .ConfigureOptions<SwaggerConfigurator>();
}