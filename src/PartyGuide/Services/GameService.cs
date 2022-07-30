using PartyGuide.Contracts;
using PartyGuide.Contracts.Requests;
using PartyGuide.Models;
using PartyGuide.Persistence.Entities;
using PartyGuide.Persistence.Repositories;

namespace PartyGuide.Services;

public interface IGameService
{
    Task<Guid> AddGameAsync(CreateGameRequest createGameRequest);
    Task<List<Game>> GetGamesAsync(List<Equipment> selectedEquipment);
}

public class GameService : IGameService
{
    private readonly ILogger<IGameService> _logger;
    private readonly IGameRepository _gameRepository;

    public GameService(
        ILogger<IGameService> logger, 
        IGameRepository gameRepository)
    {
        _logger = logger;
        _gameRepository = gameRepository;
    }

    public async Task<Guid> AddGameAsync(CreateGameRequest createGameRequest)
    {
        var gameEntity = new GameEntity
        {
            Id = Guid.NewGuid(),
            Name = createGameRequest.Name,
            Description = createGameRequest.Description,
            RequiredEquipment = createGameRequest.RequiredEquipment
        };

        await _gameRepository.AddAsync(gameEntity);

        return gameEntity.Id;
    }

    public async Task<List<Game>> GetGamesAsync(List<Equipment> selectedEquipment)
    {
        var gameEntities = await _gameRepository.GetAsync(selectedEquipment);

        return gameEntities.Select(gameEntity => new Game(gameEntity)).ToList();
    }
}
