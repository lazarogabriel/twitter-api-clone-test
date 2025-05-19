using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using twitter.api.domain.Models;

namespace twitter.api.data.EntityConfigurations
{
    public class FollowRelationshipEntityTypeConfig : IEntityTypeConfiguration<FollowRelationship>
    {
        public void Configure(EntityTypeBuilder<FollowRelationship> builder)
        {
            builder.ToTable("FollowRelationships");

            builder.HasKey(f => new { f.FollowerId, f.FollowedId });

            builder.HasOne(f => f.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Followed)
               .WithMany(u => u.Followers)
               .HasForeignKey(f => f.FollowedId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(f => f.FollowedAt)
                .HasColumnType("timestamp with time zone");
        }
    }
}
