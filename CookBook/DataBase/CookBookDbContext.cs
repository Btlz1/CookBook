using System.Reflection;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Database;

public class CookBookDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RecipeModel> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
    
    public CookBookDbContext(
        DbContextOptions<CookBookDbContext> options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(ri => new { ri.RecipeId, ri.IngredientId });
        
        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.RecipeModel)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeId);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId);
    }
   
}