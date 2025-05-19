using System.Security.Claims;

namespace twitter.api.web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the user claim "sub" from JWT.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetSub(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
