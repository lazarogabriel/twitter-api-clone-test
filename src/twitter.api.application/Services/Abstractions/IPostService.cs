using twitter.api.domain.Models;

namespace twitter.api.application.Services.Abstractions
{
    public interface IPostService
    {
        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="creatorId">The user who creates the post.</param>
        /// <param name="description">The post description.</param>
        /// <returns></returns>
        Task<Post> CreatePost(Guid creatorId, string description);
    }
}
