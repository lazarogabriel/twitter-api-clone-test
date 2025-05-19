using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using twitter.api.domain.Models;

namespace twitter.api.data.DbContexts
{
    public interface ITwitterApiDbContext
    {
        DbSet<Post> Posts { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<FollowRelationship> FollowRelationships { get; set; }

        DbSet<AuthUser> AuthUsers { get; set; }

        DbSet<RefreshToken> RefreshTokens { get; set; }


        #region Methods

        /// <summary>
        /// Tries to commit changes asynchronously and handles concurrency issues.
        /// </summary>
        /// <param name="action">The actions that changes the database to commit.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Thrown when concurrency problem could not be handled.</exception>
        Task TryCommitAsync(Action action);

        Task CommitAsync();

        #endregion
    }
}
