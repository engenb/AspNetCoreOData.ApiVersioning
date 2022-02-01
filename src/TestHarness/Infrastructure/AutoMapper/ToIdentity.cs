using AutoMapper;
using TestHarness.Data;

namespace TestHarness.Infrastructure.AutoMapper
{
    public class ToIdentity : Profile
    {
        public ToIdentity()
        {
            CreateMap<Identity, Model.V3.Identity>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.GivenName, opts => opts.MapFrom(src => src.GivenName))
                .ForMember(dest => dest.Surname, opts => opts.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email));
        }
    }
}
