using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PartyGuide.Contracts.Requests;
using PartyGuide.Services;

namespace PartyGuide.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly IGameService _gameService;
    private readonly IValidator<CreateGameRequest> _createGameRequestValidator;
    private readonly IValidator<GetGamesRequest> _getGamesRequestValidator;

    public GameController(
        ILogger<GameController> logger,
        IGameService gameService,
        IValidator<CreateGameRequest> createGameRequestValidator,
        IValidator<GetGamesRequest> getGamesRequestValidator)
    {
        _logger = logger;
        _gameService = gameService;
        _createGameRequestValidator = createGameRequestValidator;
        _getGamesRequestValidator = getGamesRequestValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
    {
        var validationResult = await _createGameRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var gameId = await _gameService.AddGameAsync(request);

        // TODO: When an endpoint is created to get based on ID, change this to CreatedAtAction
        return Ok(gameId);
    }

    [HttpPost]
    [Route("getGames")]
    public async Task<IActionResult> GetGames([FromBody] GetGamesRequest request)
    {
        var validationResult = await _getGamesRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var games = await _gameService.GetGamesAsync(request.SelectedEquipment);

        return Ok(games);
    }
}
