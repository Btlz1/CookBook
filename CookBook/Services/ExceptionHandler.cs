using System.Net;
using System.Net.Mime;
using CookBook.Exceptions;
using Microsoft.AspNetCore.Diagnostics;


namespace CookBook.Services;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is UserNotFoundException userNotFoundException)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(userNotFoundException.Message);
            return true;
        }
        if (exception is RecipeNotFoundException recipeNotFoundException)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(recipeNotFoundException.Message);
            return true;
        }
        if (exception is IngredientNotFoundException ingredientNotFoundException)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(ingredientNotFoundException.Message);
            return true;
        }
        
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await httpContext.Response.WriteAsync(string.Empty);
        return false;
    }
}