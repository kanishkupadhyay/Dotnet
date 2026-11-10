using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Infrastructure.Persistence.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blogs");

        // Primary Key
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        // Title
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        // Content
        builder.Property(x => x.Content)
            .IsRequired();

        // ImageUrl
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500);

        // AuthorId
        builder.Property(x => x.AuthorId)
            .IsRequired();

        // Foreign Key Relationship
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}