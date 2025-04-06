using twitter.api.application.Services.Abstractions;

namespace twitter.api.application.Services
{
    public class PostService : IPostService
    {
        #region Fields

        private readonly TwitterApiDbContext _dbContext;

        #endregion

        #region Constructor

        public PostService(TwitterApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public Task<Post> CreatePost(Guid userId, string description)
        {
            var post = new Post(description);

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == issuerId);

            if (user is null)
            {
                throw new NotFoundException(Errors.UserNotFound);
            }

            user.AddPost(post);

            return post;
        }

        #endregion
    }
}
