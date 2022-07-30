using Microsoft.EntityFrameworkCore;
using PartyGuide.Contracts;
using PartyGuide.Persistence.Entities;

namespace PartyGuide.Persistence.Repositories;

public interface IGameRepository
{
    Task AddAsync(GameEntity entity);
    Task<List<GameEntity>> GetAsync(List<Equipment> selectedEquipment);
}

public class GameRepository: IGameRepository
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
}
