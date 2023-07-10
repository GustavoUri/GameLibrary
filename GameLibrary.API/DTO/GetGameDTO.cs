namespace GameLibrary.DTO;

public class GetGameDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Studio { get; set; }
    public List<string>? Genres { get; set; } 
}