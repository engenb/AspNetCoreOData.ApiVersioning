using AutoMapper;

namespace TestHarness.Infrastructure.AutoMapper
{
    public class ToUser : Profile
    {
        public ToUser()
        {
            CreateMap<Data.User, Model.V1.User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrganizationId, opts => opts.MapFrom(src => src.OrganizationId))
                .ForMember(dest => dest.Organization, opts => opts.MapFrom(src => src.Organization))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.Identity.GivenName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.Identity.Surname))
                .ForMember(dest => dest.Enabled, opts => opts.MapFrom(src => src.Enabled));

            CreateMap<Data.User, Model.V2.User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrganizationId, opts => opts.MapFrom(src => src.OrganizationId))
                .ForMember(dest => dest.Organization, opts => opts.MapFrom(src => src.Organization))
                .ForMember(dest => dest.GivenName, opts => opts.MapFrom(src => src.Identity.GivenName))
                .ForMember(dest => dest.Surname, opts => opts.MapFrom(src => src.Identity.Surname))
                .ForMember(dest => dest.Enabled, opts => opts.MapFrom(src => src.Enabled));

            CreateMap<Data.User, Model.V3.User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdentityId, opts => opts.MapFrom(src => src.IdentityId))
                .ForMember(dest => dest.Identity, opts => opts.MapFrom(src => src.Identity))
                .ForMember(dest => dest.OrganizationId, opts => opts.MapFrom(src => src.OrganizationId))
                .ForMember(dest => dest.Organization, opts => opts.MapFrom(src => src.Organization))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Roles))
                .ForMember(dest => dest.Enabled, opts => opts.MapFrom(src => src.Enabled));
        }
    }
}
