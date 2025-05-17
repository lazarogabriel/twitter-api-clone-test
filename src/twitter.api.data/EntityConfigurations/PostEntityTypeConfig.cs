using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using twitter.api.domain.Models;

namespace twitter.api.data.EntityConfigurations
{
    public class PostEntityTypeConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.Description)
                .HasMaxLength(2000)
                .IsRequired();

            builder.HasOne(u => u.Creator)
                .WithMany(p => p.Posts)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .IsRequired();
        }
    }
}
