using AutoMapper;
using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Database;
using CookBook.Exceptions;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Services.Extension;

public class ReviewRepository : IReviewRepository
{
    private readonly CookBookDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public ReviewRepository(CookBookDbContext dbContext, IMapper mapper)
        => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<ReviewVm>> GetReviewByRecipeId(int recipeId)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var listOfReview = await _dbContext.Reviews
            .Where(review => review.RecipeId == recipeId)
            .Select(review => new ReviewVm(review.Id, review.Name, review.Description, review.Score))
            .ToListAsync(token);

        return listOfReview;
    }
    
    public async Task<Review> AddReview(CreateReviewDto dto, int recipeId, int userId, Score score)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var  review = _mapper.Map<CreateReviewDto, Review>(dto);
        review.UserId = userId;
        review.RecipeId = recipeId;
        review.Score = score;
        await _dbContext.Reviews.AddAsync(review, token);
        await _dbContext.SaveChangesAsync(token);
        return review;
    }

    public async Task<int> UpdateReview(int id, UpdateReviewDto dto, Score score)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var review = TryGetReviewByIdAndThrowIfNotFound(id);
        var updatedReview = _mapper.Map<(int, UpdateReviewDto), Review>((id, dto)); 
        review.Name = updatedReview.Name; 
        review.Description = updatedReview.Description;
        review.Score = score;
        
        await _dbContext.SaveChangesAsync(token);
        
        return review.Id;
    }
 
    public async Task DeleteReview(int id)
    {
        var token = new CancellationTokenSource(5000).Token;
        
        var review = TryGetReviewByIdAndThrowIfNotFound(id);
        _dbContext.Reviews.Remove(review);
        await _dbContext.SaveChangesAsync(token);
    }
    
    private Review TryGetReviewByIdAndThrowIfNotFound(int id)
    {
        var review = _dbContext.Reviews.FirstOrDefault(n => n.Id == id);
        if (review is null)
        {
            throw new RecipeNotFoundException(id);
        }
        return review;
    }
}