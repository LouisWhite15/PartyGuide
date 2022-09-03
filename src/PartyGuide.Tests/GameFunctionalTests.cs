using PartyGuide.Contracts;
using PartyGuide.Contracts.Requests;
using PartyGuide.Models;
using PartyGuide.Tests.Support;
using PartyGuide.Tests.Support.Extensions;
using PartyGuide.Tests.Support.Factories;
using PartyGuide.Tests.Support.TestData;
using Shouldly;
using System.Text;
using System.Text.Json;

namespace PartyGuide.Tests;

public class GameFunctionalTests : FunctionalTestBase, IDisposable
{
    protected override string Path => "api/Game";

    public GameFunctionalTests(IntegrationWebApplicationFactory factory) 
        : base(factory) 
    {
    }

    [Fact]
    public async Task Create_GivenValidModel_ShouldCreateGame()
    {
        // Arrange
        var createGameRequest = GameRequestFactory.CreateGameRequest();
        var jsonCreateGameRequest = JsonSerializer.Serialize(createGameRequest);

        // Act
        var response = await Client.PostAsync(Path, new StringContent(jsonCreateGameRequest, Encoding.UTF8, "application/json"));

        // Assert
        response.ShouldBeCreated();
    }

    [Theory]
    [ClassData(typeof(InvalidCreateGameRequestTestData))]
    public async Task Create_GivenInvalidModel_ShouldNotCreateGame(Action<CreateGameRequest> invalidatingAction)
    {
        // Arrange
        var createGameRequest = GameRequestFactory.CreateGameRequest(invalidatingAction);
        var jsonCreateGameRequest = JsonSerializer.Serialize(createGameRequest);

        // Act
        var response = await Client.PostAsync(Path, new StringContent(jsonCreateGameRequest, Encoding.UTF8, "application/json"));

        // Assert
        response.ShouldBeBadRequest();
    }

    [Fact]
    public async Task GetGames_GivenEquipment_ShouldReturnGamesWithMatchingRequiredEquipment()
    {
        // Arrange
        // Create game with required equipment of cups
        var createCupGameRequest = GameRequestFactory.CreateGameRequest(createGameRequest => {
            createGameRequest.RequiredEquipment = new List<Equipment> { Equipment.Cups };
            createGameRequest.Name = "Cup Game";
            createGameRequest.Description = "Cup Game Description";
        });
        var jsonCreateCupGameRequest = JsonSerializer.Serialize(createCupGameRequest);
        await Client.PostAsync(Path, new StringContent(jsonCreateCupGameRequest, Encoding.UTF8, "application/json"));

        // Create game with required equipment of cards
        var createCardGameRequest = GameRequestFactory.CreateGameRequest(createGameRequest => createGameRequest.RequiredEquipment = new List<Equipment> { Equipment.Cards });
        var jsonCreateCardGameRequest = JsonSerializer.Serialize(createCardGameRequest);
        await Client.PostAsync(Path, new StringContent(jsonCreateCardGameRequest, Encoding.UTF8, "application/json"));

        // Prepare request to get games you can play with cups
        var getGamesRequest = GameRequestFactory.GetGamesRequest(getGamesRequest => getGamesRequest.SelectedEquipment = new List<Equipment> { Equipment.Cups });
        var jsonGetGamesRequest = JsonSerializer.Serialize(getGamesRequest);

        // Act
        var response = await Client.PostAsync($"{Path}/getGames", new StringContent(jsonGetGamesRequest, Encoding.UTF8, "application/json"));

        // Assert
        response.ShouldBeOk();

        var stringContent = await response.Content.ReadAsStringAsync();
        var games = JsonSerializer.Deserialize<List<Game>>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        games.ShouldNotBeNull();
        games.ShouldBeLike(new []
        {
            new
            {
                Name = createCupGameRequest.Name,
                Description = createCupGameRequest.Description,
                RequiredEquipment = createCupGameRequest.RequiredEquipment
            }
        });
    }

    [Fact]
    public async Task GetGames_GivenNoEquipment_ShouldNotReturnAnyGames()
    {
        // Arrange
        var createGameRequest = GameRequestFactory.CreateGameRequest();
        var jsonCreateGameRequest = JsonSerializer.Serialize(createGameRequest);
        await Client.PostAsync(Path, new StringContent(jsonCreateGameRequest, Encoding.UTF8, "application/json"));

        var getGamesRequest = GameRequestFactory.GetGamesRequest(getGamesRequest => getGamesRequest.SelectedEquipment = new List<Equipment>());
        var jsonGetGamesRequest = JsonSerializer.Serialize(getGamesRequest);

        // Act
        var response = await Client.PostAsync($"{Path}/getGames", new StringContent(jsonGetGamesRequest, Encoding.UTF8, "application/json"));

        // Assert
        response.ShouldBeOk();

        var stringContent = await response.Content.ReadAsStringAsync();
        var games = JsonSerializer.Deserialize<List<Game>>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        games.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetGame_ById_ShouldReturnGame()
    {
        // Arrange
        var createGameRequest = GameRequestFactory.CreateGameRequest();
        var jsonCreateGameRequest = JsonSerializer.Serialize(createGameRequest);
        var createGameResponse = await Client.PostAsync(Path, new StringContent(jsonCreateGameRequest, Encoding.UTF8, "application/json"));
        var createGameResponseString = await createGameResponse.Content.ReadAsStringAsync();
        var gameId = JsonSerializer.Deserialize<CreatedGameResponse>(createGameResponseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Id;

        // Act
        var response = await Client.GetAsync($"{Path}/{gameId}");

        // Assert
        response.ShouldBeOk();

        var stringContent = await response.Content.ReadAsStringAsync();
        var game = JsonSerializer.Deserialize<Game>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        game.ShouldNotBeNull();
        game.ShouldBeLike(new
        {
            Id = gameId,
            Name = createGameRequest.Name,
            Description = createGameRequest.Description,
            RequiredEquipment = createGameRequest.RequiredEquipment
        });
    }

    [Fact]
    public async Task DeleteGame_ById_ShouldDeleteGame()
    {
        // Arrange
        var createGameRequest = GameRequestFactory.CreateGameRequest();
        var jsonCreateGameRequest = JsonSerializer.Serialize(createGameRequest);
        var createGameResponse = await Client.PostAsync(Path, new StringContent(jsonCreateGameRequest, Encoding.UTF8, "application/json"));
        var createGameResponseString = await createGameResponse.Content.ReadAsStringAsync();
        var gameId = JsonSerializer.Deserialize<CreatedGameResponse>(createGameResponseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Id;

        // Act
        var response = await Client.DeleteAsync($"{Path}/{gameId}");

        // Assert
        response.ShouldBeOk();

        DbContext.Games.ShouldBeEmpty();
    }

    [Fact]
    public async Task UpdateGame_GivenValidModel_ShouldUpdateGame()
    {
        // Arrange
        var createGameRequest = GameRequestFactory.CreateGameRequest();
        var jsonCreateGameRequest = JsonSerializer.Serialize(createGameRequest);
        var createGameResponse = await Client.PostAsync(Path, new StringContent(jsonCreateGameRequest, Encoding.UTF8, "application/json"));
        var createGameResponseString = await createGameResponse.Content.ReadAsStringAsync();
        var gameId = JsonSerializer.Deserialize<CreatedGameResponse>(createGameResponseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Id;

        var updateGameRequest = GameRequestFactory.UpdateGameRequest();
        var jsonUpdateGameRequest = JsonSerializer.Serialize(updateGameRequest);

        // Act
        var response = await Client.PatchAsync($"{Path}/{gameId}", new StringContent(jsonUpdateGameRequest, Encoding.UTF8, "application/json"));

        // Assert
        response.ShouldBeOk();
        
        var game = DbContext.Games.ShouldHaveSingleItem();
        game.ShouldBeLike(new
        {
            Id = gameId,
            Name = updateGameRequest.Name,
            Description = updateGameRequest.Description,
            RequiredEquipment = updateGameRequest.RequiredEquipment
        });
    }

    [Theory]
    [ClassData(typeof(InvalidUpdateGameRequestTestData))]
    public async Task UpdateGame_GivenInvalidModel_ShouldNotUpdateGame(Action<UpdateGameRequest> invalidatingAction)
    {
        // Arrange
        var createGameRequest = GameRequestFactory.CreateGameRequest();
        var jsonCreateGameRequest = JsonSerializer.Serialize(createGameRequest);
        var createGameResponse = await Client.PostAsync(Path, new StringContent(jsonCreateGameRequest, Encoding.UTF8, "application/json"));
        var createGameResponseString = await createGameResponse.Content.ReadAsStringAsync();
        var gameId = JsonSerializer.Deserialize<CreatedGameResponse>(createGameResponseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?.Id;

        var updateGameRequest = GameRequestFactory.UpdateGameRequest(invalidatingAction);
        var jsonUpdateGameRequest = JsonSerializer.Serialize(updateGameRequest);

        // Act
        var response = await Client.PatchAsync($"{Path}/{gameId}", new StringContent(jsonUpdateGameRequest, Encoding.UTF8, "application/json"));

        // Assert
        response.ShouldBeBadRequest();
    }

    [Fact]
    public async Task Get_ShouldReturnAllGames()
    {
        // Arrange
        var createGameRequest = GameRequestFactory.CreateGameRequest();
        var jsonCreateGameRequest = JsonSerializer.Serialize(createGameRequest);
        await Client.PostAsync(Path, new StringContent(jsonCreateGameRequest, Encoding.UTF8, "application/json"));

        var createGameRequestTwo = GameRequestFactory.CreateGameRequest(createGameRequest => createGameRequest.Name = "Test Game 2");
        var jsonCreateGameRequestTwo = JsonSerializer.Serialize(createGameRequestTwo);
        await Client.PostAsync(Path, new StringContent(jsonCreateGameRequestTwo, Encoding.UTF8, "application/json"));

        // Act
        var response = await Client.GetAsync(Path);

        // Assert
        response.ShouldBeOk();

        var stringContent = await response.Content.ReadAsStringAsync();
        var games = JsonSerializer.Deserialize<List<Game>>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        games.ShouldNotBeNull();
        games.ShouldBeLike(new []
        {
            new
            {
                Name = createGameRequest.Name,
                Description = createGameRequest.Description,
                RequiredEquipment = createGameRequest.RequiredEquipment
            },
            new
            {
                Name = createGameRequestTwo.Name,
                Description = createGameRequestTwo.Description,
                RequiredEquipment = createGameRequestTwo.RequiredEquipment
            },
        });
    }
}

internal class CreatedGameResponse
{
    public Guid Id { get; set; }
}