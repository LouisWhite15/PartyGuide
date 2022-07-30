using PartyGuide.Contracts;
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

    public GameEntity()
    {
    }

    public GameEntity(Game game)
    {
        Id = game.Id;
        Name = game.Name;
        Description = game.Description;
        RequiredEquipment = game.RequiredEquipment;
    }
}
