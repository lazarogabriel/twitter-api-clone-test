using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using twitter.api.application.Models.Security;
using twitter.api.application.Services.Abstractions;
using twitter.api.web.Models.Requests;
using twitter.api.web.Models.Responses;

namespace twitter.api.web.Controllers
{
    [ApiController]
    [Route("Security")]
    public class SecurityController : ControllerBase
    {
        #region Fields

        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public SecurityController(ISecurityService securityService, IMapper mapper)
        {
            _securityService = securityService;
            _mapper = mapper;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Gets an access token and a refresh token for a given user.
        /// The access token must be used to access resources.
        /// The refresh token must be used to request a new access token from time to time.
        /// </summary>
        /// <param name="request">User credentials.</param>
        /// <returns>Access token and refresh token.</returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var tokenResult = await _securityService.Login(
                new LoginCommand(
                    email: request.Email, 
                    password: request.Password));

            return Ok(_mapper.Map<LoginResponse>(tokenResult));
        }

        /// <summary>
        /// Gets an access token and a refresh token for a given user, when the client has a refresh token
        /// tha has not expired yet.
        /// </summary>
        /// <param name="request">Refresh token.</param>
        /// <returns>Access token and refresh token.</returns>
        [HttpPost("Refresh")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var tokenResult = await _securityService.RefreshToken(refreshToken: request.RefreshToken);

            return Ok(_mapper.Map<LoginResponse>(tokenResult));
        }

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="request">User information.</param>
        /// <returns>Access token and refresh token.</returns>
        [HttpPost("Register")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var tokenResult = await _securityService.Register(
                new RegisterCommand(
                    userName: request.UserName,
                    email: request.Email,
                    password: request.Password));

            return Ok(_mapper.Map<LoginResponse>(tokenResult));
        }

        #endregion
    }
}
