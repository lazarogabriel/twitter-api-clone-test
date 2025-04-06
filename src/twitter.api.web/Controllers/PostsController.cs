using Microsoft.AspNetCore.Mvc;

namespace twitter.api.web.Controllers
{
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Fields

        private readonly IPostService _postService;

        #endregion

        #region Constructor

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        #endregion

        #region Endpoints

        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            var post = await _postService.CreatePost(request.Description);

            return CreatedAtAction()
        }

        #endregion
    }
}
