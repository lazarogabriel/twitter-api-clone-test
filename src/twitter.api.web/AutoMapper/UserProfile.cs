using AutoMapper;
using twitter.api.domain.Models;
using twitter.api.web.Models.Responses;

namespace twitter.api.web.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<FollowRelationship, CreateFollowerResponse>()
               .ForMember(d => d.FollowerId, o => o.MapFrom(s => s.Follower.Id))
               .ForMember(d => d.FollowedId, o => o.MapFrom(s => s.Followed.Id))
               .ForMember(d => d.FollowedAt, o => o.MapFrom(s => s.FollowedAt));
        }
    }
}
