namespace CookBook.Models;

public class RecipeIngredient
{
    public int RecipeId { get; set; }
    public RecipeModel RecipeModel { get; set; }

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    public double Quantity { get; set; } 
    public string Unit { get; set; } 
}