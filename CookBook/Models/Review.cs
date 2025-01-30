using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace CookBook.Models;

public class  Review
{
    public int Id { get; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public Score Score { get; set; }
}

public enum Score
{
    One = 1,
    Two,
    Three,
    Four,
    Five
}

