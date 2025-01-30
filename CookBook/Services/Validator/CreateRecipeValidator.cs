using CookBook.Contracts;
using FluentValidation;

namespace SimpleExample.Services.Validators;

public class CreateRecipeValidator : AbstractValidator<CreateRecipeDto>
{
    public CreateRecipeValidator()
    {
        RuleFor(createDto => createDto.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(24);
        
        RuleFor(createDto => createDto.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(256);
        
        RuleFor(recipe => recipe.Ingredients)
            .Empty().WithMessage("должен быть хотя бы 1 ингредиент");
    }
}