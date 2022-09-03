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
    private readonly IValidator<UpdateGameRequest> _updateGameRequestValidator;

    public GameController(
        ILogger<GameController> logger,
        IGameService gameService,
        IValidator<CreateGameRequest> createGameRequestValidator,
        IValidator<GetGamesRequest> getGamesRequestValidator,
        IValidator<UpdateGameRequest> updateGameRequestValidator)
    {
        _logger = logger;
        _gameService = gameService;
        _createGameRequestValidator = createGameRequestValidator;
        _getGamesRequestValidator = getGamesRequestValidator;
        _updateGameRequestValidator = updateGameRequestValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
    {
        var validationResult = await _createGameRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var gameId = await _gameService.AddAsync(request);

        return CreatedAtAction(nameof(Get), new { id = gameId });
    }

    [HttpPost]
    [Route("getGames")]
    public async Task<IActionResult> GetGames([FromBody] GetGamesRequest request)
    {
        var validationResult = await _getGamesRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var games = await _gameService.GetAsync(request.SelectedEquipment);

        return Ok(games);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var game = await _gameService.GetAsync(id);

        if (game == null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _gameService.DeleteAsync(id);

        return Ok();
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGameRequest request)
    {
        var validationResult = await _updateGameRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        await _gameService.UpdateAsync(id, request);

        return Ok();
    }
}
