using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace TestHarness.Infrastructure.Configuration
{
    public class ODataConfigurator : IConfigureOptions<ODataOptions>
    {
        private readonly Lazy<IEdmModel> _v1Model;
        private IEdmModel V1Model => _v1Model.Value;

        private readonly Lazy<IEdmModel> _v2Model;
        private IEdmModel V2Model => _v2Model.Value;
        
        private readonly Lazy<IEdmModel> _v3Model;
        private IEdmModel V3Model => _v3Model.Value;

        public ODataConfigurator()
        {
            _v1Model = new Lazy<IEdmModel>(BuildV1Model);
            _v2Model = new Lazy<IEdmModel>(BuildV2Model);
            _v3Model = new Lazy<IEdmModel>(BuildV3Model);
        }

        public void Configure(ODataOptions options)
        {
            options.EnableQueryFeatures(500);

            options.AddRouteComponents("sample/v1", V1Model);
            options.AddRouteComponents("sample/v2", V2Model);
            options.AddRouteComponents("sample/v3", V3Model);
        }

        private static IEdmModel BuildV1Model() =>
            new ODataConventionModelBuilder()
                .EnableLowerCamelCase()
                .BuildV1UserModel()
                .BuildV1OrganizationModel()
                .GetEdmModel();

        private static IEdmModel BuildV2Model() =>
            new ODataConventionModelBuilder()
                .EnableLowerCamelCase()
                .BuildV2UserModel()
                .BuildV1OrganizationModel()
                .GetEdmModel();

        private static IEdmModel BuildV3Model() =>
            new ODataConventionModelBuilder()
                .EnableLowerCamelCase()
                .BuildV3UserModel()
                .BuildV3OrganizationModel()
                .GetEdmModel();
    }

    public static class ODataModelBuilderExtensions
    {
        #region V1

        public static ODataModelBuilder BuildV1UserModel(this ODataModelBuilder builder)
        {
            var usersSet = builder.EntitySet<Model.V1.User>($"{nameof(Model.V1.User)}s");
            var userType = usersSet.EntityType;

            userType.HasKey(x => x.Id);
            userType.HasRequired(x => x.Organization);

            userType
                .Count()
                .Select()
                .Expand()
                .Filter()
                .OrderBy()
                .Page();

            return builder;
        }

        public static ODataModelBuilder BuildV1OrganizationModel(this ODataModelBuilder builder)
        {
            var organizationsSet = builder.EntitySet<Model.V1.Organization>($"{nameof(Model.V1.Organization)}s");
            var organizationType = organizationsSet.EntityType;

            organizationType.HasKey(x => x.Id);

            organizationType
                .Count()
                .Select()
                .Expand()
                .Filter()
                .OrderBy()
                .Page();

            return builder;
        }

        #endregion V1

        #region V2

        public static ODataModelBuilder BuildV2UserModel(this ODataModelBuilder builder)
        {
            var usersSet = builder.EntitySet<Model.V2.User>($"{nameof(Model.V2.User)}s");

            var userType = usersSet.EntityType;

            userType.HasKey(x => x.Id);
            userType.HasRequired(x => x.Organization);

            userType
                .Count()
                .Select()
                .Expand()
                .Filter()
                .OrderBy()
                .Page();

            return builder;
        }

        #endregion V2

        #region V3

        public static ODataModelBuilder BuildV3UserModel(this ODataModelBuilder builder)
        {
            var usersSet = builder.EntitySet<Model.V3.User>($"{nameof(Model.V3.User)}s");

            var userType = usersSet.EntityType;

            userType.HasKey(x => x.Id);
            userType.HasRequired(x => x.Identity);
            userType.HasRequired(x => x.Organization);

            userType
                .Count()
                .Select()
                .Expand()
                .Filter()
                .OrderBy()
                .Page();

            return builder;
        }

        public static ODataModelBuilder BuildV3OrganizationModel(this ODataModelBuilder builder)
        {
            var organizationsSet = builder.EntitySet<Model.V3.Organization>($"{nameof(Model.V3.Organization)}s");
            var organizationType = organizationsSet.EntityType;

            organizationType.HasKey(x => x.Id);
            organizationType.HasMany(x => x.Users);

            organizationType
                .Count()
                .Select()
                .Expand()
                .Filter()
                .OrderBy()
                .Page();

            return builder;
        }

        #endregion V3
    }
}
