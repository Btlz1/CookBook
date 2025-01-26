using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Abstractions;

public interface IRecipeRepository
{
    Task<List<RecipeVm>> GetRecipes(int userId);
    Task<RecipeModel> AddRecipe(CreateRecipeDto dto, int userId);
    Task<int> UpdateRecipe(int id, UpdateRecipeDto dto);
    Task DeleteRecipe(int id);
}