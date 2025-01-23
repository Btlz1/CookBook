using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Database;
using CookBook.Exceptions;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CookBook.Services;

public class IngredientRepository : IIngredientPepository 
{
    private readonly CookBookDbContext _dbContext;
    private readonly IMapper _mapper;
    
    
    public IngredientRepository(CookBookDbContext dbContext, IMapper mapper)
        => (_dbContext, _mapper ) = (dbContext, mapper);

    public async Task<List<Ingredient>> GetIngredientsByRecipe(int recipeId)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var ingredientsList = await _dbContext.Ingredients
            .Where(ingredient => ingredient.RecipeIngredients
                .Any(ri => ri.RecipeId == recipeId)) 
            .ToListAsync(token);

        return ingredientsList;
    }

    public async Task<List<Ingredient>> GetAllIngredients()
    {
        var token = new CancellationTokenSource(5000).Token;

        var listOfIngredients = await _dbContext.Ingredients
            .ToListAsync(token);

        return listOfIngredients;
    }
    
    public async Task<Ingredient> AddIngredient(Ingredient dto)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var  ingredient = _mapper.Map<Ingredient>(dto);
        await _dbContext.Ingredients.AddAsync(ingredient, token);
        await _dbContext.SaveChangesAsync(token);
        return ingredient;
    }
    
    public async Task DeleteIngredient(int id)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var ingredient = TryGetIngredientByIdAndThrowIfNotFound(id);
        _dbContext.Ingredients.Remove(ingredient);
        await _dbContext.SaveChangesAsync(token);
    }
    
    private Ingredient TryGetIngredientByIdAndThrowIfNotFound(int id)
    {
        var ingredient = _dbContext.Ingredients.FirstOrDefault(n => n.IngredientId == id);
        if (ingredient is null)
        {
            throw new IngredientNotFoundException(id);
        }
        return ingredient;
    }
}