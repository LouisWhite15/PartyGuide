using PartyGuide.Contracts;
using PartyGuide.Contracts.Requests;
using PartyGuide.Models;
using PartyGuide.Persistence.Entities;
using PartyGuide.Persistence.Repositories;

namespace PartyGuide.Services;

public interface IGameService
{
    Task<Guid> AddAsync(CreateGameRequest createGameRequest);
    Task<List<Game>> GetAsync(List<Equipment> selectedEquipment);
    Task<Game?> GetAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateGameRequest updateGameRequest);
    Task DeleteAsync(Guid id);
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

    public async Task<Guid> AddAsync(CreateGameRequest createGameRequest)
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

    public async Task<List<Game>> GetAsync(List<Equipment> selectedEquipment)
    {
        var gameEntities = await _gameRepository.GetAsync(selectedEquipment);

        return gameEntities.Select(gameEntity => new Game(gameEntity)).ToList();
    }

    public async Task<Game?> GetAsync(Guid id)
    {
        var game = await _gameRepository.GetAsync(id);

        return game == null
            ? null
            : new Game(game);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _gameRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(Guid id, UpdateGameRequest updateGameRequest)
    {
        var gameEntity = new GameEntity
        {
            Id = id,
            Name = updateGameRequest.Name,
            Description = updateGameRequest.Description,
            RequiredEquipment = updateGameRequest.RequiredEquipment
        };

        await _gameRepository.UpdateAsync(id, gameEntity);
    }
}
