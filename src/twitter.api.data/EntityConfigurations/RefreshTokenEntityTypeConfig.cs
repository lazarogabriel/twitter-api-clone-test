using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using twitter.api.domain.Models;

namespace twitter.api.data.EntityConfigurations
{
    public class RefreshTokenEntityTypeConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(r => r.ExpiresAt)
                .IsRequired();

            builder.HasOne(x => x.AuthUser)
                .WithMany()
                .IsRequired();
        }
    }
}
