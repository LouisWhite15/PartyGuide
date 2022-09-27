using System;
using PartyGuide.Contracts.Tournament;
using PartyGuide.Models;

namespace PartyGuide.Services;

public interface ITournamentService
{
    Tournament Generate(GenerateTournamentRequest request);
}

public class TournamentService : ITournamentService
{
    private readonly ILogger<TournamentService> _logger;

    public TournamentService(ILogger<TournamentService> logger)
    {
        _logger = logger;
    }

    public Tournament Generate(GenerateTournamentRequest request)
    {
        _logger.LogInformation("Generating tournament {name} with {participantCount} participants", request.Name, request.Participants.Count);

        var participantsCopy = request.Participants.Select(participant => new Participant
        {
            Name = participant.Name
        }).ToList();

        var matches = new List<Match>();

        for (var i = 0; i < request.Participants.Count / 2; i ++)
        {
            var firstParticipant = participantsCopy.First();
            var lastParticipant = participantsCopy.Last();

            var match = new Match(new List<Participant> { firstParticipant, lastParticipant });
            matches.Add(match);

            participantsCopy.Remove(firstParticipant);
            participantsCopy.Remove(lastParticipant);
        }

        return new Tournament
        {
            Name = request.Name,
            Participants = request.Participants,
            Matches = matches
        };
    }
}


