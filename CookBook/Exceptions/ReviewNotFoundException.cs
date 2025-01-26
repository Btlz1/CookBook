namespace CookBook.Exceptions;

public class ReviewNotFoundException: Exception
{
    public ReviewNotFoundException(int id) : base($"Review with id = {id} not found") { }
}