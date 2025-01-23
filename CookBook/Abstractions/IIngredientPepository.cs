using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Abstractions;

public interface IIngredientPepository
{
    Task<List<Ingredient>> GetIngredientsByRecipe(int recipeId);
    Task<List<Ingredient>> GetAllIngredients();
    Task<Ingredient> AddIngredient(Ingredient dto);
    Task DeleteIngredient(int id);
} 