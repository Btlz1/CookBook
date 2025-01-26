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

public class IngredientsController : BaseController
{
    private readonly IIngredientPepository _ingredientRepository;
   
    public IngredientsController(IIngredientPepository ingredientRepository) 
        => _ingredientRepository = ingredientRepository;

    [HttpGet]
    [Route("api/ListOfIngredients")]
    public async Task<ActionResult<List<IngredientRC>>> GetAllIngredients()
    {
        var ingredients = await _ingredientRepository.GetAllIngredients();
        return Ok(ingredients);
    }
    
    [HttpGet]
    [Route("api/IngredientsInRecipe")]
    public async Task<ActionResult<IngredientRC>> GetIngredientsByRecipe(int recipeId) 
        => Ok(await _ingredientRepository.GetIngredientsByRecipe(recipeId));
    
    [HttpPost]
    public async Task<ActionResult<Ingredient>> AddIngredient(IngredientRC dto)
    {
        await _ingredientRepository.AddIngredient(dto);
        return  Ok(dto);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteIngredient(int id)
    {
        await _ingredientRepository.DeleteIngredient(id);
        return NoContent();
    }
}
