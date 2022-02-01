using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace AspNetCore.OData.ApiVersioning.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddODataApiVersioning(this IMvcBuilder mvc)
        {
            mvc.Services.AddODataApiVersioning();
            return mvc;
        }

        public static IServiceCollection AddODataApiVersioning(this IServiceCollection services)
        {
            services.TryAddSingleton<ODataApiVersioningModelProvider>();

            services.AddOptions<ODataOptions>()
                .Configure<ODataApiVersioningModelProvider>((opts, provider) => opts.Conventions.Add(provider));

            return services;
        }
    }
}
