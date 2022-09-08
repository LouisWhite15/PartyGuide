using PartyGuide.Contracts;
using PartyGuide.Contracts.Requests;
using PartyGuide.Models;
using System.ComponentModel.DataAnnotations;

namespace PartyGuide.Persistence.Entities;

public class GameEntity
{
    [Required]
    [Key]
    public Guid Id { get; set; } = Guid.Empty;

    [Required]
    [StringLength(PersistenceConstants.NameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(PersistenceConstants.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public List<Equipment> RequiredEquipment { get; set; } = new();

    [Required]
    [StringLength(PersistenceConstants.RulesMaxLength)]
    public string Rules { get; set; } = string.Empty;

    public GameEntity()
    {
    }

    public GameEntity(Game game)
    {
        Id = game.Id;
        Name = game.Name;
        Description = game.Description;
        RequiredEquipment = game.RequiredEquipment;
        Rules = game.Rules;
    }

    public GameEntity(CreateGameRequest createGameRequest)
    {
        Id = Guid.NewGuid();
        Name = createGameRequest.Name;
        Description = createGameRequest.Description;
        RequiredEquipment = createGameRequest.RequiredEquipment;
        Rules = createGameRequest.Rules;
    }

    public GameEntity(Guid id, UpdateGameRequest updateGameRequest)
    {
        Id = id;
        Name = updateGameRequest.Name;
        Description = updateGameRequest.Description;
        RequiredEquipment = updateGameRequest.RequiredEquipment;
        Rules = updateGameRequest.Rules;
    }
}
