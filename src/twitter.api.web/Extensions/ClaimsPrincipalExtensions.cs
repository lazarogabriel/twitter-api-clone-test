using System.Security.Claims;

namespace twitter.api.web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the user id from JWT.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue("userId");
        }
    }
}
