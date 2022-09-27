using System;

namespace PartyGuide.Models;

public class Tournament
{
    public string Name { get; set; } = string.Empty;
    public List<Participant> Participants { get; set; } = new();
    public List<Match> Matches { get; set; } = new();
}

public class Participant
{
    public string Name { get; set; } = string.Empty;
}

public class Match
{
    public List<Participant> Participants { get; set; } = new();
    public Participant? Winner { get; set; } = null;

    public Match(List<Participant> participants)
    {
        Participants = participants;
    }
}
