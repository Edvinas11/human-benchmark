using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
        public DbSet<GameConfig> GameConfigs { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Target> Targets { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameConfig>()
                        .Property(g => g.GameType)
                        .HasConversion<string>();

            modelBuilder.Entity<Game>()
                        .HasOne<GameConfig>()
                        .WithMany()
                        .HasForeignKey(g => g.GameConfigId);

            modelBuilder.Entity<Game>()
                        .HasMany(g => g.Targets)
                        .WithOne()
                        .HasForeignKey(t => t.GameId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Score>().HasData(
                new Score(1, 100, DateTime.UtcNow, GameType.MovingTargets),
                new Score(2, 150, DateTime.UtcNow, GameType.ReflexTest),
                new Score(3, 130, DateTime.UtcNow, GameType.CustomChallenge),
                new Score(4, 175, DateTime.UtcNow, GameType.MovingTargets)
            );


        }
    }
}
