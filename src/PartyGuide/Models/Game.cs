using PartyGuide.Contracts;
using PartyGuide.Persistence.Entities;

namespace PartyGuide.Models;

public class Game
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Equipment> RequiredEquipment { get; set; } = new();

    public Game()
    {
    }

    public Game(GameEntity gameEntity)
    {
        Id = gameEntity.Id;
        Name = gameEntity.Name;
        Description = gameEntity.Description;
        RequiredEquipment = gameEntity.RequiredEquipment;
    }
}
