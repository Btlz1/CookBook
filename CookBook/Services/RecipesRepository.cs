using AutoMapper;
using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Database;
using CookBook.Exceptions;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Services;

public class RecipesRepository : IRecipeRepository 
{
    private readonly CookBookDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public RecipesRepository(CookBookDbContext dbContext, IMapper mapper)
        => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<RecipeVm>> GetRecipes(int userId)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var listOfRecipes = await _dbContext.Recipes
            .Where(recipe => recipe.User.Id == userId)
            .Select(recipe => new RecipeVm(recipe.Id, recipe.Name, recipe.Description))
            .ToListAsync(token);

        return listOfRecipes;
    }
    
    public async Task<RecipeModel> AddRecipe(CreateRecipeDto dto, int userId)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var  recipe = _mapper.Map<RecipeModel>(dto);
        recipe.UserId = userId;
        recipe.DateCreated = DateTime.UtcNow;
        recipe.Finished = false;
        await _dbContext.Recipes.AddAsync(recipe, token);
        await _dbContext.SaveChangesAsync(token);
        return recipe;
    }

    public async Task<int> UpdateRecipe(int id, UpdateRecipeDto dto)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var recipe = TryGetRecipeByIdAndThrowIfNotFound(id);
        var updatedRecipe = _mapper.Map<(int, UpdateRecipeDto), RecipeModel>((id, dto));
        recipe.Name = updatedRecipe.Name; 
        recipe.Description = updatedRecipe.Description;
        recipe.Finished = true;
        recipe.EditDate = DateTime.UtcNow;
        foreach (var ingredientDto in dto.Ingredients)
        {
            await UpdateRecipeIngredient(ingredientDto.IngredientId, id, ingredientDto);
        }
        
        await _dbContext.SaveChangesAsync(token);
        
        return recipe.Id;
    }
 
    public async Task DeleteRecipe(int id)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var note = TryGetRecipeByIdAndThrowIfNotFound(id);
        _dbContext.Recipes.Remove(note);
        await _dbContext.SaveChangesAsync(token);
    }
    
    private RecipeModel TryGetRecipeByIdAndThrowIfNotFound(int id)
    {
        var recipe = _dbContext.Recipes.FirstOrDefault(n => n.Id == id);
        if (recipe is null)
        {
            throw new RecipeNotFoundException(id);
        }
        return recipe;
    }
    
    private async Task UpdateRecipeIngredient(int ingredientId, int recipeId, IngredientVm dto)
    {
        var token = new CancellationTokenSource(5000).Token;
        var ingredient = await _dbContext.RecipeIngredient
            .FirstOrDefaultAsync(i => i.IngredientId == ingredientId && i.Id == recipeId, token);

        if (ingredient == null)
        {
            throw new KeyNotFoundException($"Ingredient with id {ingredientId} not found.");
        }
        
        ingredient.Quantity = dto.Quantity;
        ingredient.Unit = dto.Unit.Trim();
        
        await _dbContext.SaveChangesAsync(token);
    }
}