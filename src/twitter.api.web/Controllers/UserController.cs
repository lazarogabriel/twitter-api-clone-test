using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using twitter.api.application.Services.Abstractions;
using twitter.api.domain.Models;
using twitter.api.web.Models;

namespace twitter.api.web.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Creates a follower.
        /// </summary>
        /// <param name="userId">The person who's bieng followed.</param>
        /// <returns></returns>
        [HttpPost("{userId}/Followers")]
        [ProducesResponseType(typeof(CreateFollowerResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> FollowUser(Guid userId)
        {
            var currentUserId = "GetUserId()";
            var followRelationship = await _userService.CreateFollower(
                followerId: new Guid(currentUserId),
                userToFollowId: userId);

            return CreatedAtAction(
                nameof(GetFollower),
                routeValues: new { id = followRelationship.Id },
                _mapper.Map<FollowRelationshipResponse>(followRelationship));
        }

        /// <summary>
        /// Deletes a follower.
        /// </summary>
        /// <param name="userId">The person who's bieng unfollowed.</param>
        /// <returns></returns>
        [HttpPost("{userId}/Followers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFollower([FromBody] Guid userId)
        {
            var currentUserId = "GetUserId()";

            await _userService.DeleteFollower(
                unfollowerId: new Guid(currentUserId),
                userToUnfollowId: userId);

            return NoContent();
        }

        [HttpGet("{followerId}")]
        public async Task<IActionResult> GetFollower([FromRoute] Guid followerId)
        {
            throw new NotImplementedException("Get is not implemented yet.");
        }

        #endregion
    }
}
