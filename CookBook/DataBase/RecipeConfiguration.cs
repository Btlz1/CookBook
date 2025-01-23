using CookBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Database;

public class RecipeConfiguration : IEntityTypeConfiguration<RecipeModel>
{
    public void Configure(EntityTypeBuilder<RecipeModel> builder)
    {
        builder.HasKey(note => note.Id);
        builder.Property(user => user.Name).HasMaxLength(128);
        builder.Property(user => user.Description).HasMaxLength(1024);
    }
}