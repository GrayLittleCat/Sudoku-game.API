using Domain.Players;
using FluentValidation;

namespace Application.Players.Register;

public sealed class RegisterPlayerCommandValidator : AbstractValidator<RegisterPlayerCommand>
{
    public RegisterPlayerCommandValidator(IPlayerRepository playerRepository)
    {
        RuleFor(c => c.Email).MustAsync(async (email, _) =>
                await playerRepository.IsEmailUniqueAsync(email))
            .WithMessage("Email should be unique.");
    }
}
