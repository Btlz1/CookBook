namespace CookBook.Models;

public class Reviev
{
    public int Id { get; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Stars Stars { get; set; } 
    public int RecipeId { get; set; }
}

public enum Stars
{
    Five = 5,
    Four = 4,
    Three = 3,
    Two = 2,
    One = 1
}