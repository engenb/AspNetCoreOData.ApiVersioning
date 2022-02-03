using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;

namespace AspNetCore.OData.ApiVersioning.ApiExplorer
{
    public class ODataApiVersioningDescriptionProvider : IApiDescriptionProvider
    {
        public int Order => int.MaxValue;

        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ICompositeMetadataDetailsProvider _compositeMetadataDetailsProvider;

        private readonly IOptions<ODataOptions> _odataOptions;
        private ODataOptions ODataOptions => _odataOptions.Value;

        public ODataApiVersioningDescriptionProvider(
            IModelMetadataProvider modelMetadataProvider,
            ICompositeMetadataDetailsProvider compositeMetadataDetailsProvider,
            IOptions<ODataOptions> odataOptions)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _compositeMetadataDetailsProvider = compositeMetadataDetailsProvider;
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

                    apiDescription.ParameterDescriptions.Add(BuildQueryParameter<string>("$select"));
                    apiDescription.ParameterDescriptions.Add(BuildQueryParameter<string>("$expand"));
                    apiDescription.ParameterDescriptions.Add(BuildQueryParameter<string>("$filter"));
                    apiDescription.ParameterDescriptions.Add(BuildQueryParameter<string>("$orderby"));
                    apiDescription.ParameterDescriptions.Add(BuildQueryParameter<int>("$top"));
                    apiDescription.ParameterDescriptions.Add(BuildQueryParameter<int>("$skip"));
                    apiDescription.ParameterDescriptions.Add(BuildQueryParameter<bool>("$count"));
                }
            }
        }

        private ApiParameterDescription BuildQueryParameter<TParameter>(string name)
        {
            var bindingInfo = new BindingInfo
            {
                BindingSource = BindingSource.Query
            };

            return new()
            {
                BindingInfo = bindingInfo,
                DefaultValue = null,
                IsRequired = false,
                Name = name,
                ModelMetadata = _modelMetadataProvider.GetMetadataForType(typeof(TParameter)),
                ParameterDescriptor = new ControllerParameterDescriptor
                {
                    BindingInfo = bindingInfo,
                    Name = name,
                    ParameterType = typeof(TParameter)
                },
                Source = BindingSource.Query,
                Type = typeof(TParameter)
            };
        }
    }
}
