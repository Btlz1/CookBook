using CookBook.Abstractions;

namespace CookBook.Services;

public static class IngredientsRepositoryExtension
{
    public static IServiceCollection AddIngredientRepository(this IServiceCollection services)=>
        services.AddScoped<IIngredientPepository, IngredientRepository>();
}