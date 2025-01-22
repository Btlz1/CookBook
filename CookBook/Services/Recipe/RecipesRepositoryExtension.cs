using CookBook.Services;
using CookBook.Abstractions;

namespace CookBook.Services.Recipe;

public static class RecipesRepositoryExtension
{
    public static IServiceCollection AddRecipesRepository(this IServiceCollection services)=>
        services.AddScoped<IRecipeRepository, RecipesRepository>();
}