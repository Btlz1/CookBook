using CookBook.Abstractions;

namespace CookBook.Services;

public static class IngredientRepositoryExtension
{
    public static IServiceCollection AddIngredientRepository(this IServiceCollection services)=>
        services.AddScoped<IIngredientPepository, IngredientRepository>();
}