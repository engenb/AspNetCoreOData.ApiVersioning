using AutoMapper;
using TestHarness.Data;

namespace TestHarness.Infrastructure.AutoMapper
{
    public class ToRole : Profile
    {
        public ToRole()
        {
            CreateMap<Role, string>()
                .ConvertUsing<RoleConverter>();
        }
    }

    public class RoleConverter : ITypeConverter<Role, string>
    {
        public string Convert(Role source, string destination, ResolutionContext context) =>
            source;
    }
}
