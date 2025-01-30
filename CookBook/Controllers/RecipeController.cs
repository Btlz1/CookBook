using Microsoft.AspNetCore.Mvc;
using CookBook.Abstractions;
using CookBook.Contracts;
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
    
    [HttpGet("GetRecipesByCategory")]
    public async Task<ActionResult<Recipe>> GetRecipesByCategory(Category category)
        => Ok(await _recipeRepository.GetRecipesByCategory(category));

    [HttpPost]
    public async Task<ActionResult<RecipeVm>> AddRecipe(CreateRecipeDto dto, int userId, Category category)
        => Ok(await _recipeRepository.AddRecipe(dto, userId, category));
    
    [HttpPut("{id}")]
    [Authorize(Policy = "RecipeOwner")]
    public async Task<ActionResult<int>> UpdateRecipe(int userId, int id, UpdateRecipeDto dto, Category category)
        => Ok(await _recipeRepository.UpdateRecipe(id, dto, category));
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "RecipeOwner")]
    public async Task<ActionResult> DeleteRecipe(int userId, int id)
    {
        await _recipeRepository.DeleteRecipe(id);
        return NoContent();
    }
}