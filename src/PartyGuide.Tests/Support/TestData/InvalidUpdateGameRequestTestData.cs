using PartyGuide.Contracts.Requests;

namespace PartyGuide.Tests.Support.TestData;

internal class InvalidUpdateGameRequestTestData : TheoryData<Action<UpdateGameRequest>>
{
    public InvalidUpdateGameRequestTestData()
    {
        Add(updateGameRequest => updateGameRequest.Name = string.Empty);
        Add(updateGameRequest => updateGameRequest.Description = string.Empty);
        Add(updateGameRequest => updateGameRequest.RequiredEquipment = new());
    }
}
