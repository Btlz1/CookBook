using System.Reflection;
using CookBook.Configuration.DataBase;
using CookBook.Database;
using CookBook.Models;
using CookBook.Services;
using CookBook.Services.Extension;
using CookBook.Services.Recipe;
using CookBook.Settings;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;


namespace CookBook.Composer;

public static class Composer
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
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
        services.AddReviewRepository();
        
        return services;
    }
    
}