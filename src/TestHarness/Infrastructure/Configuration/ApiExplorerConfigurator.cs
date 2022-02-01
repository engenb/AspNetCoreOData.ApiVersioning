using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

namespace TestHarness.Infrastructure.Configuration
{
    public class ApiExplorerConfigurator : IConfigureOptions<ApiExplorerOptions>
    {
        public void Configure(ApiExplorerOptions options)
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        }
    }
}
