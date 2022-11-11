using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PartyGuide.Contracts.Tournament;
using PartyGuide.Services;
using PartyGuide.Validators;

namespace PartyGuide.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentController : ControllerBase
{
    private readonly ILogger<TournamentController> _logger;
    private readonly ITournamentService _tournamentService;
    private readonly IValidator<GenerateTournamentRequest> _generateTournamentValidator;

    public TournamentController(
        ILogger<TournamentController> logger,
        ITournamentService tournamentService,
        IValidator<GenerateTournamentRequest> createTournamentValidator)
    {
        _logger = logger;
        _tournamentService = tournamentService;
        _generateTournamentValidator = createTournamentValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Generate([FromBody] GenerateTournamentRequest request)
    {
        var validationResult = await _generateTournamentValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var tournament = _tournamentService.Generate(request);

        return Ok(tournament);
    }
}
