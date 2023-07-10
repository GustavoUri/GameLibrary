using AutoMapper;
using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Interfaces.Services;
using GameLibrary.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers;

[ApiController]
//[Route("Games")]
public class GameLibraryController : Controller
{
    private readonly IMapper _mapper;
    private readonly IGameService _gameService;

    public GameLibraryController(IGameService gameService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new game
    /// </summary>
    /// <param name="gameDTO">Body for the game creation</param>
    /// <returns>A newly created game</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /AddGame
    ///     {
    ///        "name": "Fallout 4",
    ///        "studio": "Bethesda",
    ///         "genres": ["Action", "Crafting"]
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created game</response>
    /// <response code="400">If json is not full, or name of the game is already taken</response>
    [HttpPost("AddGame")]
    public async Task<IActionResult> AddGame([FromBody] CreateGameDTO gameDTO, CancellationToken cancellationToken)
    {
        var game = _mapper.Map<Game>(gameDTO);
        var addedGame = await _gameService.AddGameAsync(game, cancellationToken);
        //return StatusCode(_mapper.Map<GetGameDTO>(addedGame));
        return Created(nameof(AddGame), _mapper.Map<GetGameDTO>(addedGame));
    }

    /// <summary>
    /// Updates a game
    /// </summary>
    /// <param name="gameId">"Id of the game"</param>
    /// <param name="gameDTO">Body for the game update</param>
    /// <returns>A newly created game</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /UpdateGame
    ///     parameter - gameId=374b85ae-35f9-49ea-9ac3-3da7e7a3b04f
    ///     {
    ///        "name": "Fallout 4",
    ///        "studio": "Bethesda",
    ///         "genres": ["Action", "Crafting"]
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the updated game</response>
    /// <response code="400">If json is not full, or name of the game is already taken</response>
    [HttpPut("UpdateGame")]
    public async Task<IActionResult> UpdateGame(Guid gameId, [FromBody] UpdateGameDTO gameDTO,
        CancellationToken cancellationToken)
    {
        var game = _mapper.Map<Game>(gameDTO);
        var updatedGame = await _gameService.UpdateGameAsync(gameId, game, cancellationToken);
        return Ok(_mapper.Map<GetGameDTO>(updatedGame));
    }

    /// <summary>
    /// Deletes a game
    /// </summary>
    /// <param name="gameId">Id of the game</param>
    /// <returns>A deleted game</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /DeleteGame
    ///     parameter - gameId : "374b85ae-35f9-49ea-9ac3-3da7e7a3b04f"
    ///     
    ///
    /// </remarks>
    /// <response code="200">Returns the deleted game</response>
    /// <response code="400">If json is not full, or there is no game with this id</response>
    [HttpDelete("DeleteGame")]
    public async Task<IActionResult> DeleteGame(Guid gameId, CancellationToken cancellationToken)
    {
        var deletedGame = await _gameService.DeleteGameAsync(gameId, cancellationToken);
        return Ok(_mapper.Map<GetGameDTO>(deletedGame));
    }

    /// <summary>
    /// Returns games with or without filter
    /// </summary>
    /// <param name="filterDto">Filter for games</param>
    /// <returns>A Games with or without filter</returns>
    /// <remarks>
    /// 
    /// Sample request:
    ///
    ///     GET /GetGames
    ///     parameters
    ///     gameGenres=Fighting
    ///     gameGenres=Shooter
    ///     
    ///     if filter is null controller returns all games
    /// </remarks>
    /// <response code="200">Returns games</response>
    /// <response code="400">If json is not full</response>
    [HttpGet("GetGames")]
    public async Task<IActionResult> GetGames([FromQuery] GameFilterDTO filterDto, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<GameFilter>(filterDto);
        var games = await _gameService.GetGames(cancellationToken, filter);
        var gamesDTO = games.Select(game => _mapper.Map<GetGameDTO>(game));
        return Ok(gamesDTO);
    }
}