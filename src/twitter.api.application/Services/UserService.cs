using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using twitter.api.application.Services.Abstractions;
using twitter.api.data.DbContexts;
using twitter.api.domain.Constants;
using twitter.api.domain.Exceptions;
using twitter.api.domain.Models;

namespace twitter.api.application.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly ITwitterApiDbContext _dbContext;

        #endregion

        #region Constructor

        public UserService(ITwitterApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public async Task<FollowRelationship> CreateFollower(Guid followerId, Guid userToFollowId)
        {
            if (followerId == userToFollowId)
            {
                throw new ValidationException(Errors.CannotFollowYourself);
            }

            var follower = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == followerId);

            if (follower is null)
            {
                throw new NotFoundException(Errors.FollowerUserNotFound);
            }
            
            var userToFollow = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userToFollowId);

            if (userToFollow is null)
            {
                throw new NotFoundException(Errors.UserToFollowNotFound);
            }

            var alreadyFollowing = await _dbContext.FollowRelationships.AnyAsync(r => 
                r.Follower.Id == followerId && 
                r.Followed.Id == userToFollowId);

            if (alreadyFollowing)
            {
                throw new ValidationException(Errors.AlreadyFollowingUser);
            }

            var followRelationship = follower.Follow(userToFollow);

            await _dbContext.FollowRelationships.AddAsync(followRelationship);

            await _dbContext.CommitAsync();

            return followRelationship;
        }

        /// <inheritdoc/>
        public async Task DeleteFollower(Guid unfollowerId, Guid userToUnfollowId)
        {
            var followRelationship = await _dbContext.FollowRelationships.FirstOrDefaultAsync(r =>
                r.Follower.Id == unfollowerId &&
                r.Followed.Id == userToUnfollowId);

            if (followRelationship is null)
            {
                throw new NotFoundException(Errors.FollowRelationshipNotFound);
            }


        }

        #endregion
    }
}
