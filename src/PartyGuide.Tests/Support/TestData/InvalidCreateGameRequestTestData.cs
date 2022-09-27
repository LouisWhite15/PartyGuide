using PartyGuide.Contracts.Game;

namespace PartyGuide.Tests.Support.TestData;

internal class InvalidCreateGameRequestTestData : TheoryData<Action<CreateGameRequest>>
{
    public InvalidCreateGameRequestTestData()
    {
        Add(createGameRequest => createGameRequest.Name = string.Empty);
        Add(createGameRequest => createGameRequest.Description = string.Empty);
        Add(createGameRequest => createGameRequest.RequiredEquipment = new());
        Add(createGameRequest => createGameRequest.Rules = string.Empty);
    }
}
