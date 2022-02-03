using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.OData.ApiVersioning.ApiExplorer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddODataApiVersioningApiExplorer(this IMvcBuilder builder)
        {
            builder.Services.AddODataApiVersioningApiExplorer();
            return builder;
        }
            

        public static IServiceCollection AddODataApiVersioningApiExplorer(this IServiceCollection services) =>
            services.AddTransient<IApiDescriptionProvider, ODataApiVersioningDescriptionProvider>();
    }
}
