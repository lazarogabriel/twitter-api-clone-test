using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using twitter.api.domain.Models;

namespace twitter.api.data.EntityConfigurations
{
    public class UserEntityTypeConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.Creator);

            builder.Property(u => u.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .IsRequired();
        }
    }
}
