using CookBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Database;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Login).HasMaxLength(64);
        builder.Property(user => user.Password).HasMaxLength(256);
    }
}