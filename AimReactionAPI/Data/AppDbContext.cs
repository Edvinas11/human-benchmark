using AimReactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AimReactionAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
        public DbSet<User> Users { get; set; }
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
                        .WithMany()
                        .HasForeignKey(s => s.GameId);
        }
    }
}
