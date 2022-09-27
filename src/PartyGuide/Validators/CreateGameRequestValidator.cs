using FluentValidation;
using PartyGuide.Contracts.Game;

namespace PartyGuide.Validators;

public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameRequestValidator()
    {
        RuleFor(createGameRequest => createGameRequest.Name).NotEmpty();
        RuleFor(createGameRequest => createGameRequest.Description).NotEmpty();
        RuleFor(createGameRequest => createGameRequest.RequiredEquipment).NotEmpty();
        RuleFor(createGameRequest => createGameRequest.Rules).NotEmpty();
    }
}
