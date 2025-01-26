using CookBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Database;

public class RecipeConfiguration : IEntityTypeConfiguration<RecipeModel>
{
    public void Configure(EntityTypeBuilder<RecipeModel> builder)
    {
        builder.HasKey(recipe => recipe.Id);
        builder.Property(recipe => recipe.Name).HasMaxLength(128);
        builder.Property(recipe => recipe.Description).HasMaxLength(1024);
    }
}