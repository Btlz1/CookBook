namespace CookBook.Models;

public class RecipeIngredient
{
    public int Id { get; set; }
    public Recipe Recipe { get; set; }

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    public double Quantity { get; set; } 
    public string Unit { get; set; } 
}