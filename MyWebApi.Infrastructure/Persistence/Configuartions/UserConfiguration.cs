
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        // Primary Key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        // First Name
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        // Last Name
        builder.Property(x => x.LastName)
            .HasMaxLength(100);

        // Email
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);

        // Unique Email Index
        builder.HasIndex(x => x.Email)
            .IsUnique();

        // Password Hash
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);
    }
}
