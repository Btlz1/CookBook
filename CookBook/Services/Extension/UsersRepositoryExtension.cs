using CookBook.Abstractions;

namespace CookBook.Services;

public static class UsersRepositoryExtension
{
    public static IServiceCollection AddUserRepository(this IServiceCollection services)=>
        services.AddScoped<IUserRepository, UserRepository>();
}