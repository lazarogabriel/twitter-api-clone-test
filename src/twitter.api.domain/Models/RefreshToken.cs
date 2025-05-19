using System;

namespace twitter.api.domain.Models
{
    public class RefreshToken
    {
        /// <summary>
        /// EF constructor.
        /// </summary>
        public RefreshToken() { }

        public RefreshToken(DateTime expiresAt, AuthUser authUser)
        {
            ExpiresAt = expiresAt;
            AuthUser = authUser;
        }

        public Guid Id { get; }

        public DateTime ExpiresAt { get; }

        public AuthUser AuthUser { get; }
    }
}
