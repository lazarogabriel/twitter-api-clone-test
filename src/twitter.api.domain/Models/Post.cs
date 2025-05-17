using Microsoft.VisualBasic;
using System;
using twitter.api.domain.Constants;
using twitter.api.domain.Exceptions;
namespace twitter.api.domain.Models
{
    public class Post
    {
        #region Fields

        private string _description;

        #endregion

        #region Constructor

        /// <summary>
        /// Ef core constructor.
        /// </summary>
        public Post()
        {
            
        }

        public Post(string description, User creator)
        {
            Description = description;
            Creator = creator;
            CreatedAt = DateTime.UtcNow;
        }

        #endregion

        #region Properties

        public Guid Id { get; }

        public User Creator { get; }

        public string Description 
        { 
            get => _description; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidParameterException(Errors.PostDescriptionCannotBeNullOrWhiteSpace);
                }

                var trimmedValue = value.Trim();

                if (value.Length > 2000)
                {
                    throw new InvalidParameterException(Errors.PostDescriptionCannotBeMoreThan2000Chars);
                }

                _description = trimmedValue;
            } 
        }

        public DateTime CreatedAt { get; }

        #endregion
    }
}
