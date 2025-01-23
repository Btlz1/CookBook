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
    public async Task<ActionResult<RecipeModel>> GetRecipes(int userId) 
        => Ok(await _recipeRepository.GetRecipes(userId));
    
    [HttpPost]
    public async Task<ActionResult<RecipeModel>> AddRecipe(CreateRecipeDto dto)
    {
        RecipeModel recipeModel = new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Finished = false,
            DateCreated = DateTime.UtcNow,
            UserId = HttpContext.ExtractUserIdFromClaims()!.Value,
            RecipeIngredients = dto.Ingredients.Select(ingredientDto => new RecipeIngredient
            {
                IngredientId = ingredientDto.IngredientId, 
                Quantity = ingredientDto.Quantity,
                Unit = ingredientDto.Unit
            }).ToList()
        };
        await _recipeRepository.AddRecipe(recipeModel);
        return  Ok(recipeModel);
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