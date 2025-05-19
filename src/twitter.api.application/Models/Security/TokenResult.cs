using System;

namespace twitter.api.application.Models.Security
{
    public class TokenResult
    {
        public TokenResult(string token, DateTime expiresAt, Guid refreshToken)
        {
            Token = token;
            Expiration = expiresAt;
            RefreshToken = refreshToken;
        }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public Guid RefreshToken { get; set; }
    }
}
