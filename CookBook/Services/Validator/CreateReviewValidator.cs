using CookBook.Contracts;
using FluentValidation;

namespace SimpleExample.Services.Validators;

public class CreateReviewValidator: AbstractValidator<CreateReviewDto>
{
    public CreateReviewValidator()
    {
        RuleFor(createDto => createDto.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(30);
        
        RuleFor(createDto => createDto.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(256);
    }
}