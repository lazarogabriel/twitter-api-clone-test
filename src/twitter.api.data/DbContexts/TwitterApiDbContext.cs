using Microsoft.EntityFrameworkCore;
using twitter.api.domain.Models;

namespace twitter.api.data.DbContexts
{
    public class TwitterApiDbContext : DbContext, ITwitterApiDbContext
    {
        #region Constructor

        public TwitterApiDbContext(DbContextOptions options) : base(options)
        {
        }

        #endregion

        #region Props

        public DbSet<Post> Posts { get ; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<FollowRelationship> FollowRelationships { get ; set; }
        
        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public async Task CommitAsync() => await SaveChangesAsync();

        /// <inheritdoc/>
        public async Task TryCommitAsync(Action action)
        {
            var commited = false;
            var attempts = 0;
            while (!commited && attempts <= 3)
            {
                try
                {
                    // Attempt to commit changes to the database
                    attempts++;
                    await CommitAsync();
                    commited = true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    action();
                }
            }
        }

        #endregion
    }
}
