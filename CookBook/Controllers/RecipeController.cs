using Microsoft.AspNetCore.Mvc;
using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Exceptions;
using CookBook.Models;
using Microsoft.AspNetCore.Authorization;

namespace CookBook.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class RecipeController : BaseController
{
    private readonly IRecipeRepository _recipeRepository;
   
    public RecipeController(IRecipeRepository recipeRepository) 
        => _recipeRepository = recipeRepository;

    [HttpGet]
    [Authorize(Policy = "RecipeOwner")]
    public async Task<ActionResult<Recipe>> GetRecipes(int userId) 
        => Ok(await _recipeRepository.GetRecipes(userId));
    
    [HttpPost]
    public async Task<ActionResult<Recipe>> AddRecipe(CreateRecipeDto dto)
    {
        Recipe recipe = new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Finished = false,
            DateCreated = DateTime.UtcNow,
            UserId = HttpContext.ExtractUserIdFromClaims()!.Value,
        };
        await _recipeRepository.AddRecipe(recipe);
        return  Ok(recipe);
    }
    
    [HttpPut("{id}")]
    [Authorize(Policy = "RecipeOwner")]
    public async Task<ActionResult<int>> UpdateRecipe(int userId, int id, UpdateRecipeDto dto)
        => Ok(await _recipeRepository.UpdateRecipe(id, dto));
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "RecipeOwner")]
    public async Task<ActionResult> DeleteRecipe(int userId, int id)
    {
        await _recipeRepository.DeleteRecipe(id);
        return NoContent();
    }
}