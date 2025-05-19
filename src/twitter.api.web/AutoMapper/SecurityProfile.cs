using AutoMapper;
using twitter.api.application.Models.Security;
using twitter.api.web.Models.Responses;

namespace twitter.api.web.AutoMapper
{
    public class SecurityProfile : Profile
    {
        public SecurityProfile()
        {
            CreateMap<TokenResult, LoginResponse>()
                .ForMember(d => d.Token, o => o.MapFrom(s => s.Token))
                .ForMember(d => d.RefreshToken, o => o.MapFrom(s => s.RefreshToken))
                .ForMember(d => d.Expiration, o => o.MapFrom(s => s.Expiration));
        }
    }
}
