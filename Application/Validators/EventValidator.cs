using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class EventValidator : AbstractValidator<EventDto>
{
    public EventValidator()
    {
        RuleFor(r => r.Name)!.NotEmpty()!.WithMessage("Name is required.");
        RuleFor(r => r.Name)!.MinimumLength(3).MaximumLength(20)
            .WithMessage("Name must be at least 3 but no more than 20 characters");
        
        RuleFor(r => r.Date)!.NotEmpty()!.WithMessage("Date is required.");
        RuleFor(r => r.Date)!.GreaterThan(DateTime.Now).WithMessage("Date must be later than today.");
        RuleFor(r => r.OrganizerId)!.NotEmpty()!.WithMessage("Organizer is required.");
    }
}