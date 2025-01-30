using FluentValidation;

namespace CookBook.Models;

public class User
{
    public int Id { get; set;}
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}

