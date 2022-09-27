namespace PartyGuide.Contracts.Game;

public class GetGamesRequest
{
    public List<Equipment> SelectedEquipment { get; set; } = new();
}
