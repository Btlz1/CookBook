using System.Reflection;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Database;

public class CookBookDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    
    public CookBookDbContext(
        DbContextOptions<CookBookDbContext> options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
   
}