using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
        public DbSet<User> Users { get; set; }
        public DbSet<GameConfig> GameConfigs { get; set; } 
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Scores)
                        .WithOne()
                        .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<GameConfig>()
                        .Property(g => g.GameType)
                        .HasConversion<string>();
        }
    }
}
