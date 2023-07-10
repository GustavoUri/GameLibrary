using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.DTO;

public class GameFilterDTO
{
    [FromQuery(Name = "gameGenres")]
    public List<string>? GameGenres { get; set; }

}