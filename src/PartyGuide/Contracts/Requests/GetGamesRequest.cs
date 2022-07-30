namespace PartyGuide.Contracts.Requests;

public class GetGamesRequest
{
    public List<Equipment> SelectedEquipment { get; set; } = new();
}
