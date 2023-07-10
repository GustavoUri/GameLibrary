using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Exceptions;
using GameLibrary.Domain.Interfaces.Repositories;
using GameLibrary.Domain.Interfaces.Services;

namespace GameLibrary.Domain.Services;

public class GameService : IGameService
{
    private readonly IRepository<Game> _gameRepository;
    private readonly IRepository<Studio> _studioRepository;
    private readonly IRepository<Genre> _genreRepository;

    public GameService(IRepository<Game> gameRepository, IRepository<Studio> studioRepository,
        IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
        _studioRepository = studioRepository;
        _gameRepository = gameRepository;
    }

    public async Task<Game> AddGameAsync(Game game, CancellationToken cancellationToken)
    {
        await CheckGameNameForRepeating(game.Name, cancellationToken);
        await CheckGameStudioForRepeating(game, cancellationToken);
        await CheckGameGenresForRepeating(game, cancellationToken);

        await _gameRepository.AddAsync(game, cancellationToken);
        await _gameRepository.SaveAsync(cancellationToken);
        return game;
    }

    public async Task<Game> UpdateGameAsync(Guid id, Game game, CancellationToken cancellationToken)
    {
        var existingGame = await _gameRepository.GetByIdOrDefaultAsync(id, cancellationToken);
        if (existingGame == null)
            throw new GameException("No game with this id");

        await CheckGameNameForRepeatingIncludingSelfName(existingGame, game, cancellationToken);
        existingGame.Name = game.Name;
        await CheckGameStudioForRepeating(game, cancellationToken);
        existingGame.Studio = game.Studio;
        await CheckGameGenresForRepeating(game, cancellationToken);
        existingGame.Genres = game.Genres;
        await _gameRepository.SaveAsync(cancellationToken);
        return existingGame;
    }

    public async Task<Game> DeleteGameAsync(Guid gameId, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByIdOrDefaultAsync(gameId, cancellationToken);
        if (game == null)
            throw new GameException("No game with this id");

        _gameRepository.Delete(game);
        await _gameRepository.SaveAsync(cancellationToken);
        return game;
    }

    public async Task<IEnumerable<Game>> GetGames(CancellationToken cancellationToken, GameFilter? filter = null)
    {
        var games = (await _gameRepository.GetAllAsync(cancellationToken)).ToList();
        if (filter == null)
            return games;
        games = FiltrateGames(games, filter);

        return games;
    }

    private async Task CheckGameNameForRepeating(string gameName, CancellationToken cancellationToken)
    {
        var gameWithSameName =
            (await _gameRepository.GetAllAsync(cancellationToken)).FirstOrDefault(x => x.Name == gameName);
        if (gameWithSameName != null)
            throw new GameException("Game with this name already exists");
    }

    private async Task CheckGameNameForRepeatingIncludingSelfName(Game existingGame, Game game,
        CancellationToken cancellationToken)
    {
        var gameWithSameName =
            (await _gameRepository.GetAllAsync(cancellationToken)).Where(x => x.Name != existingGame.Name)
            .FirstOrDefault(x => x.Name == game.Name);
        if (gameWithSameName != null)
            throw new GameException("Game with this name already exists");
    }

    private async Task CheckGameStudioForRepeating(Game game, CancellationToken cancellationToken)
    {
        var studio =
            (await _studioRepository.GetAllAsync(cancellationToken)).FirstOrDefault(x => x.Name == game.Studio.Name);
        if (studio != null)
            game.Studio = studio;
    }

    private async Task CheckGameGenresForRepeating(Game game, CancellationToken cancellationToken)
    {
        var genres = (await _genreRepository.GetAllAsync(cancellationToken)).ToList();

        for (var i = 0; i < game.Genres.Count; i++)
        {
            var genreWithSameName = genres.FirstOrDefault(genre => genre.Name == game.Genres[i].Name);
            if (genreWithSameName != null)
                game.Genres[i] = genreWithSameName;
        }
    }

    private List<Game> FiltrateGames(List<Game> games, GameFilter filter)
    {
        if (filter.GameGenres != null && filter.GameGenres.Any())
        {
            games = games.Where(game => (filter.GameGenres
                                            .Intersect(game.Genres
                                                .Select(genre => genre.Name))).Count() ==
                                        filter.GameGenres.Count)
                .ToList();
        }

        return games;
    }
}