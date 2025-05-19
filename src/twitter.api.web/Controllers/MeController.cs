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
        /// Creates a follower.
        /// </summary>
        /// <param name="userId">The person who's bieng followed.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Followers/{userId}")]
        [ProducesResponseType(typeof(CreateFollowerResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> FollowUser(Guid userId)
        {
            var currentUserId = "GetUserId()";
            var followRelationship = await _userService.CreateFollower(
                followerId: new Guid(currentUserId),
                userToFollowId: userId);

            return CreatedAtAction(
                nameof(GetFollower),
                routeValues: new { id = followRelationship.FollowedId },
                _mapper.Map<CreateFollowerResponse>(followRelationship));
        }

        /// <summary>
        /// Deletes a follower.
        /// </summary>
        /// <param name="userId">The person who's bieng unfollowed.</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("Followers/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFollower(Guid userId)
        {
            var currentUserId = "GetUserId()";

            await _userService.DeleteFollower(
                unfollowerId: new Guid(currentUserId),
                userToUnfollowId: userId);

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
            var userId = "GetUserId()";
            var post = await _postService.CreatePost(creatorId: new Guid(userId), description: request.Description);

            return CreatedAtAction(
                nameof(GetPost),
                routeValues: new { id = post.Id },
                _mapper.Map<PostResponse>(post));
        }

        [Authorize]
        [HttpGet("Posts/{postId}")]
        public async Task<IActionResult> GetPost([FromRoute] Guid postId)
        {
            throw new NotImplementedException("Get is not implemented yet.");
        }


        [Authorize]
        [HttpGet("Followers/{followerId}")]
        public async Task<IActionResult> GetFollower([FromRoute] Guid followerId)
        {
            throw new NotImplementedException("Get is not implemented yet.");
        }

        #endregion
    }
}
