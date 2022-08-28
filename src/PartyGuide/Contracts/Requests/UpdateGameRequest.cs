using System;
namespace PartyGuide.Contracts.Requests;

public class UpdateGameRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Equipment> RequiredEquipment { get; set; } = new();
}
