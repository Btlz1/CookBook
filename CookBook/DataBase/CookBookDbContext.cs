using System.Reflection;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Database;

public class CookBookDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    public CookBookDbContext(
        DbContextOptions<CookBookDbContext> options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.User)
            .WithMany() 
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(ri => new { RecipeId = ri.Id, ri.IngredientId });
        
        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.Id);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId);
        
        modelBuilder.Entity<Review>()
            .HasOne(o => o.Recipe)          
            .WithMany(u => u.Revievs)       
            .HasForeignKey(o => o.RecipeId);
    }
   
}