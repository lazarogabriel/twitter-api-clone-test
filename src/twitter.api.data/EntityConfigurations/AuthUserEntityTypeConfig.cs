using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using twitter.api.domain.Models;

namespace twitter.api.data.EntityConfigurations
{
    public class AuthUserEntityTypeConfig : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {
            builder.ToTable("AuthUsers");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.HasOne(au => au.User)
                .WithOne()
                .IsRequired();

            builder.HasOne(au => au.User)
                .WithOne(u => u.AuthUser)
                .HasForeignKey<AuthUser>(au => au.UserId)
                .IsRequired();
        }
    }
}
