namespace CookBook.Models;

public class RecipeModel
{
    public int Id { get;}
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DateCreated { get; set; } 
    public DateTime? EditDate {get; set;}
    public bool? Finished { get; set; } 
    public int UserId { get; set; } 
    public User User { get; set; }
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<Review> Revievs { get; set; }
}