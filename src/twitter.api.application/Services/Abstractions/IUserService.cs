using System;
using System.Threading.Tasks;
using twitter.api.domain.Models;

namespace twitter.api.application.Services.Abstractions
{
    public interface IUserService 
    {
        Task<FollowRelationship> CreateFollower(Guid followerId ,Guid userToFollowId);

        Task DeleteFollower(Guid unfollowerId ,Guid userToUnfollowId);
    }
}
