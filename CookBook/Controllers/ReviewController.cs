using Microsoft.AspNetCore.Mvc;
using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Exceptions;
using CookBook.Models;
using Microsoft.AspNetCore.Authorization;


namespace CookBook.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class ReviewController : BaseController
{
    private readonly IReviewRepository _reviewRepository;
   
    public ReviewController(IReviewRepository reviewRepository) 
        => _reviewRepository = reviewRepository;

    [HttpGet]
    [Route("api/ListOfReview")]
    public async Task<ActionResult<List<ReviewVm>>> GetReviewByRecipeId(int recipeId)
    {
        var reviews = await _reviewRepository.GetReviewByRecipeId(recipeId);
        return Ok(reviews);
    }
    
    [HttpPost]
    public async Task<ActionResult<Review>> AddReview(CreateReviewDto dto, int recipeId, int userId, Score score)
    {
        await _reviewRepository.AddReview(dto, recipeId, userId, score);
        return  Ok(dto);
    }
    
    [HttpPut]
    public async Task<ActionResult<Review>> UpdateReview(int id, UpdateReviewDto dto, Score score)
    {
        await _reviewRepository.UpdateReview(id, dto, score);
        return  Ok(dto);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReview(int id)
    {
        await _reviewRepository.DeleteReview(id);
        return NoContent();
    }
}