﻿namespace GameLibrary.Domain.Entities;

public class Studio
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Game>? Games { get; set; }
}