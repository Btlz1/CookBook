namespace CookBook.Models;

public class Recipe
{
    public int Id { get;}
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DateCreated { get; set; } 
    public DateTime? EditDate {get; set;}
    public bool? Finished { get; set; } 
    public int UserId { get; set; }
    public int RevievId { get; set; }
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}