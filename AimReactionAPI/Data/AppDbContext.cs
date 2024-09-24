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
        }
    }
}
