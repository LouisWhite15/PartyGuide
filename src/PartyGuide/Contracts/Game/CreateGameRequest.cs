﻿namespace PartyGuide.Contracts.Game;

public class CreateGameRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Equipment> RequiredEquipment { get; set; } = new();
    public string Rules { get; set; } = string.Empty;
}
