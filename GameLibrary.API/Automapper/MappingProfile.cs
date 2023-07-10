using AutoMapper;
using GameLibrary.Domain.Entities;
using GameLibrary.DTO;

namespace GameLibrary.Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateGameDTO, Game>()
            .ForMember(game => game.Studio,
                dto => dto.MapFrom(src => new Studio()
                    { Name = src.GameStudio }))
            .ForMember(game => game.Genres, dto => dto.MapFrom(src => (src.Genres.Select(genre =>
                new Genre() { Name = genre })).ToList()));

        CreateMap<GameFilterDTO, GameFilter>();

        CreateMap<Game, GetGameDTO>()
            .ForMember(getGame => getGame.Name, origin => origin.MapFrom(game => game.Name))
            .ForMember(getGame => getGame.Studio, origin => origin.MapFrom(game => game.Studio.Name))
            .ForMember(getGame => getGame.Genres,
                origin => origin.MapFrom(game => game.Genres.Select(genre => genre.Name).ToList()))
            .ForMember(getGame => getGame.Id, origin => origin.MapFrom(game => game.Id));

        CreateMap<UpdateGameDTO, Game>()
            .ForMember(game => game.Studio,
                dto => dto.MapFrom(src => new Studio()
                    { Name = src.GameStudio }))
            .ForMember(game => game.Genres, dto => dto.MapFrom(src => (src.Genres.Select(genre =>
                new Genre() { Name = genre })).ToList()));
    }
}