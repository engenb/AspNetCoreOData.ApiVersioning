using AutoMapper;
using TestHarness.Data;

namespace TestHarness.Infrastructure.AutoMapper
{
    public class ToOrganization : Profile
    {
        public ToOrganization()
        {
            CreateMap<Organization, Model.V1.Organization>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.DisplayName));
            
            CreateMap<Organization, Model.V3.Organization>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Enabled, opts => opts.MapFrom(src => src.Enabled))
                .ForMember(dest => dest.SingleSignOnEnabled, opts => opts.MapFrom(src => src.SingleSignOnEnabled))
                .ForMember(dest => dest.Users, opts => opts.MapFrom(src => src.Users));
        }
    }
}
