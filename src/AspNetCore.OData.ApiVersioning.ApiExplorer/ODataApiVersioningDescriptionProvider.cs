using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;

namespace AspNetCore.OData.ApiVersioning.ApiExplorer
{
    public class ODataApiVersioningDescriptionProvider : IApiDescriptionProvider
    {
        public int Order => int.MaxValue;

        private readonly IOptions<ODataOptions> _odataOptions;
        private ODataOptions ODataOptions => _odataOptions.Value;

        public ODataApiVersioningDescriptionProvider(IOptions<ODataOptions> odataOptions)
        {
            _odataOptions = odataOptions;
        }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
        }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
            foreach (var apiDescription in context.Results)
            {
                var odataQueryOptionsParameter = apiDescription.ParameterDescriptions
                    .SingleOrDefault(p => p.Type != null
                                          && p.Type.IsConstructedGenericType
                                          && p.Type.GetGenericTypeDefinition() == typeof(ODataQueryOptions<>));

                if (odataQueryOptionsParameter != null)
                {
                    apiDescription.ParameterDescriptions.Remove(odataQueryOptionsParameter);

                    apiDescription.ParameterDescriptions.Add(new ApiParameterDescription
                    {
                        Name = "$filter",
                        Type = typeof(string)
                    });
                }
            }
        }
    }
}
