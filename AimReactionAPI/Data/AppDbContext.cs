using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Target> Targets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
                    .HasMany(g => g.Targets)
                    .WithOne()
                    .HasForeignKey(t => t.GameId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Game>()
                    .Property(g => g.GameType)
                    .HasConversion<string>();

        modelBuilder.Entity<User>()
                    .HasMany(u => u.Scores)
                    .WithOne(s => s.User)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
                    .HasOne(s => s.Game)
                    .WithMany(g => g.Scores)
                    .HasForeignKey(s => s.GameId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
                    .HasOne(s => s.User)
                    .WithMany(u => u.Scores)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
                    .Property(s => s.GameType)
                    .HasConversion<string>();

        modelBuilder.Entity<GameSession>()
                    .HasOne(gs => gs.User)
                    .WithMany(u => u.GameSessions)
                    .HasForeignKey(gs => gs.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameSession>()
                    .Property(gs => gs.GameType)
                    .HasConversion<string>();
    }
}
