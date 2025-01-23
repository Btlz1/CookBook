namespace CookBook.Contracts;

public record RecipeVm(int UserId, int Id, string Name, string Description);
public record RecipesVm(List<RecipeVm> Recipes);
public record CreateRecipeDto(string Name, string Description, List<IngredientVm> Ingredients );
public record UpdateRecipeDto(string Name, string Description, List<IngredientVm> Ingredients);