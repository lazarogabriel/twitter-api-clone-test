using System.Text.RegularExpressions;

namespace twitter.api.domain.Utilities
{
    /// <summary>
    /// Represents a set of utilities that use regular expressions
    /// to obtain their results.
    /// </summary>
    public static class RegexUtilities
    {
        /// <summary>
        /// Returns true if the indicated email has a valid email format.
        /// Otherwise, it returns false.
        /// </summary>
        /// <param name="email">The email to be validated.</param>
        /// <returns></returns>
        public static bool IsEmailValid(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
    }
}
