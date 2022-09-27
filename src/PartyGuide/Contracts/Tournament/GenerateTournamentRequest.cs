using System;
using PartyGuide.Models;

namespace PartyGuide.Contracts.Tournament;

public class GenerateTournamentRequest
{
    public string Name { get; set; } = string.Empty;
    public List<Participant> Participants { get; set; } = new();
}
