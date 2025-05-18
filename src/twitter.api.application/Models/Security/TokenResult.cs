using System;

namespace twitter.api.application.Models.Security
{
    public class TokenResult
    {
        public TokenResult(string token, DateTime expirationTime, string refreshToken)
        {
            Token = token;
            ExpirationTime = expirationTime;
            RefreshToken = refreshToken;
        }

        public string Token { get; set; }

        public DateTime ExpirationTime { get; set; }

        public string RefreshToken { get; set; }
    }
}
