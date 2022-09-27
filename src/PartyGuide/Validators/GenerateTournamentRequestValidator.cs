using System;
using FluentValidation;
using PartyGuide.Contracts.Tournament;

namespace PartyGuide.Validators;

public class GenerateTournamentRequestValidator : AbstractValidator<GenerateTournamentRequest>
{
    public GenerateTournamentRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();

        // Must be an even number of participants
        RuleFor(request => request.Participants).Must(participants => IsEven(participants.Count));
    }

    private static bool IsEven(int number)
        => number % 2 == 0;
}
