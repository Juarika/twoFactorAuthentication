using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using twoFactorAuthentication.Entities;

namespace twoFactorAuthentication.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        {
            builder.ToTable("user");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserName)
            .HasColumnType("varchar(255) COLLATE utf8mb4_unicode_ci")
            .HasMaxLength(50)
            .IsRequired();
            builder.Property(p => p.Password)
           .HasColumnName("password")
           .HasColumnType("varchar(255) COLLATE utf8mb4_unicode_ci")
           .HasMaxLength(255)
           .IsRequired();
            builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(255) COLLATE utf8mb4_unicode_ci")
            .HasMaxLength(100)
            .IsRequired();

        }

    }

}