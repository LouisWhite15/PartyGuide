using Microsoft.EntityFrameworkCore;
using PartyGuide.Contracts.Game;
using PartyGuide.Persistence.Entities;

namespace PartyGuide.Persistence.Repositories;

public interface IGameRepository
{
    Task AddAsync(GameEntity entity);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, GameEntity entity);
    Task<List<GameEntity>> GetAsync();
    Task<GameEntity?> GetAsync(Guid id);
    Task<List<GameEntity>> GetAsync(List<Equipment> selectedEquipment);
}

public class GameRepository : IGameRepository
{
    private readonly ILogger<GameRepository> _logger;
    private readonly ApplicationDbContext _dbContext;

    public GameRepository(
        ILogger<GameRepository> logger, 
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task AddAsync(GameEntity entity)
    {
        _logger.LogDebug("Adding entity of type {entityType}", nameof(GameEntity));

        await _dbContext.Games.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogDebug("Deleting entity of type {entityType} with id {id}", nameof(GameEntity), id);

        var gameEntity = await _dbContext.Games.SingleOrDefaultAsync(game => game.Id == id);

        if (gameEntity == null)
        {
            _logger.LogWarning("Could not find entity of type {entityType} with id {id}", nameof(GameEntity), id);
            return;
        }

        _dbContext.Games.Remove(gameEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<GameEntity>> GetAsync()
    {
        _logger.LogDebug("Retrieving entities of type {entityType}", nameof(GameEntity));

        return await _dbContext.Games.ToListAsync();
    }

    public async Task<List<GameEntity>> GetAsync(List<Equipment> selectedEquipment)
    {
        _logger.LogDebug("Retrieving entities of type {entityType} with equipment {requiredEquipment}", nameof(GameEntity), string.Join(',', selectedEquipment));

        // Pull the games into memory to perform filtering as our complex filtereing cannot be handled by EF
        var games = await _dbContext.Games.ToListAsync();

        var filteredGames = games
            .Where(game => !game.RequiredEquipment.Except(selectedEquipment).Any())
            .ToList();

        return filteredGames;
    }

    public async Task<GameEntity?> GetAsync(Guid id)
    {
        _logger.LogDebug("Retrieving entity of type {entityType} with id {id}", nameof(GameEntity), id);

        var game = await _dbContext.Games.SingleOrDefaultAsync(game => game.Id == id);

        if (game == null)
        {
            _logger.LogWarning("Could not find entity of type {entityType} with id {id}", nameof(GameEntity), id);
            return null;
        }

        return game;
    }

    public async Task UpdateAsync(Guid id, GameEntity entity)
    {
        _logger.LogDebug("Updating entity of type {entityType} with id {id}", nameof(GameEntity), id);

        var game = await _dbContext.Games.SingleOrDefaultAsync(game => game.Id == id);

        if (game == null)
        {
            _logger.LogWarning("Could not find entity of type {entityType} with id {id}", nameof(GameEntity), id);
            return;
        }

        game.Name = entity.Name;
        game.Description = entity.Description;
        game.RequiredEquipment = entity.RequiredEquipment;

        await _dbContext.SaveChangesAsync();
    }
}
