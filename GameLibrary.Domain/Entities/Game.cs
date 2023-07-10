namespace GameLibrary.Domain.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Studio? Studio { get; set; }
    public List<Genre>? Genres { get; set; }
}