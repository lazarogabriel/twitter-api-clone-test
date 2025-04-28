using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        private string _nickName;

        #endregion

        #region Constructor

        public User(string nickName)
        {
            CreatedAt = DateTime.UtcNow;
            NickName = nickName;
        }

        #endregion

        #region Properties

        public Guid Id { get; }

        public DateTime CreatedAt { get; }

        public string NickName
        { 
            get => _nickName; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidParameterException(Errors.UsersNickNameShouldNotBeNullOrEmpty);
                }

                var trimmedValue = value.Trim();

                if (trimmedValue.Length < 5 || trimmedValue.Length > 20)
                {
                    throw new InvalidParameterException(Errors.UserNickNameMustBeBetween5And20CharsLength);
                }

                _nickName = trimmedValue;
            }
        }

        public int FollowersCount => Followers.Count;

        public List<FollowRelationship> Followers { get; }

        public List<FollowRelationship> Following { get; }

        public List<Post> Posts { get; }

        #endregion

        #region Public Methods

        public Post CreatePost(string description)
        {
            var post = new Post(description: description, creator: this);

            Posts.Add(post);

            return post;
        }

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

        public void Unfollow(User userToUnfollow)
        {
            var followRelationship = Following.FirstOrDefault(f => f.Followed.Id == userToUnfollow.Id);
            if (followRelationship is null)
            {
                throw new ValidationException(Errors.CannotUnfollowAUserThatIsNotBeingFollowed);
            }

            Following.Remove(followRelationship);
            userToUnfollow.Followers.Remove(followRelationship);
        }

        #endregion
    }
}
