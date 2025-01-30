using CookBook.Contracts;
using FluentValidation;

namespace SimpleExample.Services.Validators;


public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(createDto => createDto.Login)
            .NotNull()
            .NotEmpty()
            .MaximumLength(128);
        
        RuleFor(createDto => createDto.Password)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256);
    }
}