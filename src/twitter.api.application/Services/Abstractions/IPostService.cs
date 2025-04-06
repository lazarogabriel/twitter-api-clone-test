namespace twitter.api.application.Services.Abstractions
{
    public interface IPostService
    {
        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="userId">The user who creates the post.</param>
        /// <param name="description">The post description.</param>
        /// <returns></returns>
        Task CreatePost(Guid userId, string description);
    }
}
