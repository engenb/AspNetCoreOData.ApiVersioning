using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using TestHarness.Infrastructure.Swagger;

namespace TestHarness.Infrastructure.Configuration
{
    public class SwaggerConfigurator :
        IConfigureOptions<SwaggerGenOptions>,
        IConfigureOptions<SwaggerOptions>,
        IConfigureOptions<SwaggerUIOptions>
    {
        private const string ServiceRoute = "Sample";

        private readonly IApiVersionDescriptionProvider _apiVersionProvider;

        public SwaggerConfigurator(IApiVersionDescriptionProvider apiVersionProvider)
        {
            _apiVersionProvider = apiVersionProvider ?? throw new ArgumentNullException(nameof(apiVersionProvider));
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.DocumentFilter<VersionFilter>();
            options.OperationFilter<DefaultResponseFilter>();

            foreach (var description in _apiVersionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = ServiceRoute,
                        Version = description.ApiVersion.ToString()
                    });
            }
        }

        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = $"{ServiceRoute}/swagger/{{documentName}}/swagger.json";
        }

        public void Configure(SwaggerUIOptions options)
        {
            options.RoutePrefix = $"{ServiceRoute}/explorer";
            options.ConfigObject.DisplayRequestDuration = true;

            foreach (var groupName in _apiVersionProvider.ApiVersionDescriptions
                         .Reverse()
                         .Select(x => x.GroupName))
            {
                options.SwaggerEndpoint(
                    $"/{ServiceRoute}/swagger/{groupName}/swagger.json",
                    $"{ServiceRoute} {groupName}");
            }
        }
    }
}
