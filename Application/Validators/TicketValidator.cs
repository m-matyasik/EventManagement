using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class TicketValidator : AbstractValidator<TicketDto>
{
    public TicketValidator()
    {
        RuleFor(r => r.TicketNumber)!.NotEmpty().WithMessage("Ticket number is required.");
    }
}