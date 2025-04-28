using AutoMapper;
using twitter.api.domain.Models;
using twitter.api.web.Models;

namespace twitter.api.web.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostResponse>()
               .ForMember(d => d.Id, o => o.MapFrom(s => s.Id)) 
               .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
        }
    }
}