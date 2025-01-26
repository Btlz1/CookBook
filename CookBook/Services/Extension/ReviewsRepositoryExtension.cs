using CookBook.Abstractions;

namespace CookBook.Services.Extension;

public static class ReviewsRepositoryExtension
{
    public static IServiceCollection AddReviewRepository(this IServiceCollection services)=>
        services.AddScoped<IReviewRepository, ReviewRepository>();
}