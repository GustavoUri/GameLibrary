using System.ComponentModel.DataAnnotations;

namespace GameLibrary.DTO;

public class CreateGameDTO
{
    [StringLength(30, MinimumLength = 1)]
    public string Name { get; set; }
    [StringLength(30, MinimumLength = 1)]
    public string GameStudio { get; set; }
    public List<string> Genres { get; set; }
}