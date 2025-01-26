namespace CookBook.Models;

public class Ingredient
{
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        
}