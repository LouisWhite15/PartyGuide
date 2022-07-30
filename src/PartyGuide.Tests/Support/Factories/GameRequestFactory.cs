using PartyGuide.Contracts.Requests;

namespace PartyGuide.Tests.Support.Factories;

internal static class GameRequestFactory
{
    internal static CreateGameRequest CreateGameRequest(Action<CreateGameRequest>? customisation = null)
    {
        var createGameRequest = new CreateGameRequest
        {
            Name = "Test Name",
            Description = "Test description",
            RequiredEquipment = new List<Contracts.Equipment> { Contracts.Equipment.Cards }
        };

        customisation?.Invoke(createGameRequest);

        return createGameRequest;
    }

    internal static GetGamesRequest GetGamesRequest(Action<GetGamesRequest>? customisation = null)
    {
        var getGamesRequest = new GetGamesRequest
        {
            SelectedEquipment = new List<Contracts.Equipment> { Contracts.Equipment.Cards }
        };

        customisation?.Invoke(getGamesRequest);

        return getGamesRequest;
    }
}
