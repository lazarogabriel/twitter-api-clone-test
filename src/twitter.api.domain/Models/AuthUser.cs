using System;
using System.Collections.Generic;
using twitter.api.domain.Constants;
using twitter.api.domain.Exceptions;
using twitter.api.domain.Utilities;

namespace twitter.api.domain.Models
{
    public class AuthUser
    {
        #region Fields

        private string _email;

        #endregion

        #region Constructor

        /// <summary>
        /// EF constructor.
        /// </summary>
        public AuthUser() 
        {
        }

        /// <summary>
        /// Domain constructor.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordHash"></param>
        /// <param name="createdAt"></param>
        /// <param name="refreshTokens"></param>
        public AuthUser(string email, string passwordHash, User user)
        {
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            User = user;
            UserId = user.Id;
        }

        #endregion

        public Guid Id { get; set; }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidParameterException(Errors.UserEmailIsRequired);
                }

                var trimmedValue = value.Trim();

                if (!RegexUtilities.IsEmailValid(trimmedValue))
                {
                    throw new ValidationException(Errors.InvalidUserEmail);
                }

                _email = trimmedValue;
            }
        }

        public string PasswordHash { get; }

        public DateTime CreatedAt { get; } 

        public User User { get; }

        public Guid UserId { get; }
    }
}
