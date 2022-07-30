using PartyGuide.Contracts.Requests;

namespace PartyGuide.Tests.Support.TestData;

internal class InvalidCreateGameRequestTestData : TheoryData<Action<CreateGameRequest>>
{
    public InvalidCreateGameRequestTestData()
    {
        Add(createGameRequest => createGameRequest.Name = string.Empty);
        Add(createGameRequest => createGameRequest.Description = string.Empty);
        Add(createGameRequest => createGameRequest.RequiredEquipment = new());
    }
}
