using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class VenueValidator : AbstractValidator<VenueDto>
{
    public VenueValidator()
    {
        RuleFor(r => r.Name)!.MinimumLength(3)!.MaximumLength(20)!
            .WithMessage("Name must contain at least 3 but no more than 20 characters.");
        RuleFor(r => r.Location)!.NotEmpty()!.WithMessage("Location can not be empty.");
    }
}