using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using twitter.api.application.Services.Abstractions;
using twitter.api.web.Extensions;
using twitter.api.web.Models;
using twitter.api.web.Models.Responses;

namespace twitter.api.web.Controllers
{
    [ApiController]
    [Route("Me")]
    public class MeController : ControllerBase
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public MeController(IUserService userService, IMapper mapper, IPostService postService)
        {
            _userService = userService;
            _mapper = mapper;
            _postService = postService;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Starts following the specified user.
        /// </summary>
        /// <param name="followedUserId">Id of the user to follow.</param>
        /// <returns>Information about the new follow relationship.</returns>
        [Authorize]
        [HttpPost("Following/{followedUserId}")]
        [ProducesResponseType(typeof(CreateFollowerResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> FollowUser([FromRoute] Guid followedUserId)
        {
            var currentUserId = new Guid(User.GetUserId());

            var followRelationship = await _userService.CreateFollower(
                followerId: currentUserId,
                userToFollowId: followedUserId);

            var response = _mapper.Map<CreateFollowerResponse>(followRelationship);

            return CreatedAtAction(
                nameof(GetFollowed),
                routeValues: new { FollowedId = followRelationship.FollowedId },
                response);
        }

        /// <summary>
        /// Unfollows the specified user.
        /// </summary>
        /// <param name="followedUserId">Id of the user to unfollow.</param>
        /// <returns>No content if unfollowed successfully.</returns>
        [Authorize]
        [HttpDelete("Following/{followedUserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UnfollowUser([FromRoute] Guid followedUserId)
        {
            var currentUserId = new Guid(User.GetUserId());

            await _userService.DeleteFollower(
                unfollowerId: currentUserId,
                userToUnfollowId: followedUserId);

            return NoContent();
        }


        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="request">Data required to create post.</param>
        /// <returns>Post just created.</returns>
        [Authorize]
        [HttpPost("Posts")]
        [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            var userId = User.GetUserId();

            var post = await _postService.CreatePost(creatorId: new Guid(userId), description: request.Description);

            return CreatedAtAction(
                nameof(GetPost),
                routeValues: new { PostId = post.Id },
                _mapper.Map<PostResponse>(post));
        }

        [Authorize]
        [HttpGet("Posts/{postId}")]
        public async Task<IActionResult> GetPost([FromRoute] Guid postId)
        {
            throw new NotImplementedException("Get is not implemented yet.");
        }


        [Authorize]
        [HttpGet("Following/{followedId}")]
        public async Task<IActionResult> GetFollowed([FromRoute] Guid followedId)
        {
            throw new NotImplementedException("Get is not implemented yet.");
        }

        #endregion
    }
}
