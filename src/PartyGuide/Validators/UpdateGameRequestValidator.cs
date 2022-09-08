using FluentValidation;
using PartyGuide.Contracts.Requests;

namespace PartyGuide.Validators;

public class UpdateGameRequestValidator : AbstractValidator<UpdateGameRequest>
{
    public UpdateGameRequestValidator()
    {
        RuleFor(updateGameRequest => updateGameRequest.Name).NotEmpty();
        RuleFor(updateGameRequest => updateGameRequest.Description).NotEmpty();
        RuleFor(updateGameRequest => updateGameRequest.RequiredEquipment).NotEmpty();
        RuleFor(updateGameRequest => updateGameRequest.Rules).NotEmpty();
    }
}
