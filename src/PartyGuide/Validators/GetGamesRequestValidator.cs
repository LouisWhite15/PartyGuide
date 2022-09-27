using FluentValidation;
using PartyGuide.Contracts.Game;

namespace PartyGuide.Validators;

public class GetGamesRequestValidator : AbstractValidator<GetGamesRequest>
{
    public GetGamesRequestValidator()
    {
        RuleFor(getGamesRequest => getGamesRequest.SelectedEquipment).NotNull();
    }
}
