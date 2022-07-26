﻿using PartyGuide.Contracts.Game;

namespace PartyGuide.Tests.Support.Factories;

internal static class GameRequestFactory
{
    internal static CreateGameRequest CreateGameRequest(Action<CreateGameRequest>? customisation = null)
    {
        var createGameRequest = new CreateGameRequest
        {
            Name = "Test Name",
            Description = "Test description",
            RequiredEquipment = new List<Equipment> { Equipment.Cards },
            Rules = "Test rules"
        };

        customisation?.Invoke(createGameRequest);

        return createGameRequest;
    }

    internal static GetGamesRequest GetGamesRequest(Action<GetGamesRequest>? customisation = null)
    {
        var getGamesRequest = new GetGamesRequest
        {
            SelectedEquipment = new List<Equipment> { Equipment.Cards }
        };

        customisation?.Invoke(getGamesRequest);

        return getGamesRequest;
    }

    internal static UpdateGameRequest UpdateGameRequest(Action<UpdateGameRequest>? customisation = null)
    {
        var updateGameRequest = new UpdateGameRequest
        {
            Name = "Updated Name",
            Description = "Updated Description",
            RequiredEquipment = new List<Equipment> { Equipment.Cups },
            Rules = "Updated rules"
        };

        customisation?.Invoke(updateGameRequest);

        return updateGameRequest;
    }
}
