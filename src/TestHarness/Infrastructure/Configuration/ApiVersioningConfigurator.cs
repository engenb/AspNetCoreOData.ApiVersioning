using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

namespace TestHarness.Infrastructure.Configuration
{
    public class ApiVersioningConfigurator : IConfigureOptions<ApiVersioningOptions>
    {
        public void Configure(ApiVersioningOptions options)
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
        }
    }
}
