﻿using System;

namespace twitter.api.domain.Models
{
    /// <summary>
    /// Representation of many to many users in a way in where a user can follow many users or can be followed by many users.
    /// </summary>
    public class FollowRelationship
    {
        /// <summary>
        /// Ef constructor.
        /// </summary>
        public FollowRelationship()
        {
            
        }

        public FollowRelationship(User follower, User followed)
        {
            Follower = follower;
            Followed = followed;
            FollowedAt = DateTime.UtcNow;
        }

        #region Properties

        /// <summary>
        /// The user who follows.
        /// </summary>
        public User Follower { get; }
        
        /// <summary>
        /// The user being followed.
        /// </summary>
        public User Followed { get; }

        /// <summary>
        /// When the follow happen.
        /// </summary>
        public DateTime FollowedAt { get; }

        /// <summary>
        /// The user who follows identifier.
        /// </summary>
        public Guid FollowerId { get; }

        /// <summary>
        /// The user being followed identifier.
        /// </summary>
        public Guid FollowedId { get; }

        #endregion
    }
}
