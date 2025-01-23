using AutoMapper;
using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Database;
using CookBook.Exceptions;
using Microsoft.EntityFrameworkCore;
using Recipe = CookBook.Models.Recipe;


namespace CookBook.Services;

public class RecipesRepository : IRecipeRepository 
{
    private readonly CookBookDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    public RecipesRepository(CookBookDbContext dbContext, IMapper mapper, IUserRepository userRepository)
        => (_dbContext, _mapper, _userRepository) = (dbContext, mapper, userRepository);

    public async Task<List<RecipeVm>> GetRecipes(int userId)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var listOfRecipes = await _dbContext.Recipes
            .Where(recipe => recipe.UserId == userId)
            .Select(recipe => new RecipeVm(recipe.UserId, recipe.Id, recipe.Name, recipe.Description))
            .ToListAsync(token);

        return listOfRecipes;
    }
    
    public async Task<Models.Recipe> AddRecipe(Models.Recipe dto)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var  note = _mapper.Map<Models.Recipe>(dto);
        await _dbContext.Recipes.AddAsync(note, token);
        await _dbContext.SaveChangesAsync(token);
        return note;
    }

    public async Task<int> UpdateRecipe(int id, UpdateRecipeDto dto)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var recipe = TryGetRecipeByIdAndThrowIfNotFound(id);
        var updatedRecipe = _mapper.Map<(int, UpdateRecipeDto), Models.Recipe>((id, dto));
        recipe.Name = updatedRecipe.Name; 
        recipe.Description = updatedRecipe.Description; 
        recipe.Finished = true;
        recipe.EditDate = DateTime.UtcNow;
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
    
    private Models.Recipe TryGetRecipeByIdAndThrowIfNotFound(int id)
    {
        var recipe = _dbContext.Recipes.FirstOrDefault(n => n.Id == id);
        if (recipe is null)
        {
            throw new RecipeNotFoundException(id);
        }
        return recipe;
    }
}