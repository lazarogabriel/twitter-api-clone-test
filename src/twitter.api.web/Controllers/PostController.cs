using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using twitter.api.application.Services.Abstractions;
using twitter.api.web.Models;
using twitter.api.web.Models.Responses;

namespace twitter.api.web.Controllers
{
    [ApiController]
    [Route("Posts")]
    public class PostController : ControllerBase
    {
        #region Fields

        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="request">Data required to create post.</param>
        /// <returns>Post just created.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            var userId = "GetUserId()";
            var post = await _postService.CreatePost(creatorId: new Guid(userId), description: request.Description);

            return CreatedAtAction(
                nameof(GetPost),
                routeValues: new { id = post.Id },
                _mapper.Map<PostResponse>(post));
        }

        [Authorize]
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost([FromRoute] Guid postId)
        {
            throw new NotImplementedException("Get is not implemented yet.");
        }

        #endregion
    }
}
