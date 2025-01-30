using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Abstractions;

public interface IRecipeRepository
{
    Task<List<RecipeVm>> GetRecipes(int userId);
    Task<List<RecipeVm>> GetRecipesByCategory(Category category);
    Task<RecipeVm> AddRecipe(CreateRecipeDto dto, int userId, Category Category);
    Task<int> UpdateRecipe(int id, UpdateRecipeDto dto, Category Category);
    Task DeleteRecipe(int id);
}