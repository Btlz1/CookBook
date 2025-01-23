using System.Reflection;
using CookBook.Configuration.DataBase;
using CookBook.Database;
using CookBook.Services;
using CookBook.Services.Recipe;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace CookBook.Composer;

public static class Composer
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.Configure<CookBookDbConnectionSettings>(
            configuration.GetRequiredSection(nameof(CookBookDbConnectionSettings)));
        services.AddDbContext<CookBookDbContext>(options =>
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var settings = scope.ServiceProvider.GetRequiredService<IOptions<CookBookDbConnectionSettings>>().Value;
            options.UseNpgsql(settings.ConnectionString);
        });
        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static IServiceCollection AddApplicationServices(this
        IServiceCollection services)
    {
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddRecipesRepository();
        services.AddUserRepository();
        services.AddIngredientRepository();
        return services;
    }
}