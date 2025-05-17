using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using twitter.api.application.Services.Abstractions;
using twitter.api.data.DbContexts;
using twitter.api.domain.Constants;
using twitter.api.domain.Exceptions;
using twitter.api.domain.Models;

namespace twitter.api.application.Services
{
    public class PostService : IPostService
    {
        #region Fields

        private readonly ITwitterApiDbContext _dbContext;

        #endregion

        #region Constructor

        public PostService(ITwitterApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async Task<Post> CreatePost(Guid creatorId, string description)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == creatorId);

            if (user is null)
            {
                throw new NotFoundException(Errors.UserNotFound);
            }

            var post = user.CreatePost(description);

            await _dbContext.Posts.AddAsync(post);
            await _dbContext.CommitAsync();
            
            return post;
        }

        #endregion
    }
}
