using GameLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GameLibrary.Infrastructure.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Studio> GameStudios { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DatabaseFacade Database => base.Database;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Game>()
            .HasOne(game => game.Studio)
            .WithMany(gameStudio => gameStudio.Games);

        modelBuilder
            .Entity<Game>()
            .HasMany(game => game.Genres)
            .WithMany(genre => genre.Games)
            .UsingEntity("GameGenre");
    }
}