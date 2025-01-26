using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Abstractions;

public interface IReviewRepository
{
    Task<List<ReviewVm>> GetReviewByRecipeId(int recipeId);
    Task<Review> AddReview(CreateReviewDto dto, int recipeId, int userId, Score score);
    Task<int> UpdateReview(int id, UpdateReviewDto dto, Score score);
    Task DeleteReview(int id);
}