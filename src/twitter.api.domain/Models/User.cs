using System;
using System.Collections.Generic;
using twitter.api.domain.Constants;
using twitter.api.domain.Exceptions;

namespace twitter.api.domain.Models
{
    /// <summary>
    /// Represetation of Twitter's User.
    /// </summary>
    public class User
    {
        #region Fields

        private string _userName;

        #endregion

        #region Constructor

        /// <summary>
        /// Ef core constructor.
        /// </summary>
        public User()
        {
            Followers = new List<FollowRelationship>();
            Following = new List<FollowRelationship>();
            Posts = new List<Post>();
        }

        public User(string userName)
        {
            CreatedAt = DateTime.UtcNow;
            UserName = userName;
            Followers = new List<FollowRelationship>();
            Following = new List<FollowRelationship>();
            Posts = new List<Post>();
        }

        #endregion

        #region Properties

        public Guid Id { get; }

        public DateTime CreatedAt { get; }

        public string UserName
        { 
            get => _userName; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidParameterException(Errors.UserNameIsRequired);
                }

                var trimmedValue = value.Trim();

                if (trimmedValue.Length < 5 || trimmedValue.Length > 20)
                {
                    throw new InvalidParameterException(Errors.UserNickNameMustBeBetween5And20CharsLength);
                }

                _userName = trimmedValue;
            }
        }

        public int FollowersCount => Followers.Count;

        public List<FollowRelationship> Followers { get; }

        public List<FollowRelationship> Following { get; }

        public List<Post> Posts { get; }

        public AuthUser AuthUser { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a post.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public Post CreatePost(string description)
        {
            var post = new Post(description: description, creator: this);

            Posts.Add(post);

            return post;
        }

        /// <summary>
        /// Starts following a user creating a relationship btw.
        /// </summary>
        /// <param name="userToFollow"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public FollowRelationship Follow(User userToFollow)
        {
            if (userToFollow.Id == Id)
            {
                throw new ValidationException(Errors.CannotFollowYourself);
            }

            var followRelationship = new FollowRelationship(follower: this, followed: userToFollow);

            Following.Add(followRelationship);
            userToFollow.Followers.Add(followRelationship);

            return followRelationship;
        }

        /// <summary>
        /// Stop following a user by removing all relationship as this user as a follower
        /// and the followed.
        /// </summary>
        /// <param name="followed"></param>
        /// <param name="relationship"></param>
        public void Unfollow(User followed, FollowRelationship relationship)
        {
            Following.Remove(relationship);
            followed.RemoveFollower(relationship);
        }

        /// <summary>
        /// Removes the relationship as a user beign followed.
        /// </summary>
        /// <param name="relationship"></param>
        public void RemoveFollower(FollowRelationship relationship)
        {
            Followers.Remove(relationship);
        }

        #endregion
    }
}
