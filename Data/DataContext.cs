using Microsoft.EntityFrameworkCore;
using ReviewApp.Models;

namespace ReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        { 

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Publisher> Publishers { get; set;}
        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GameUser> GameUsers { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameGenre>()
                .HasKey(gg => new { gg.GameId, gg.GenreId });
            modelBuilder.Entity<GameGenre>()
                .HasOne(g => g.Game)
                .WithMany(gg => gg.GameGenres)
                .HasForeignKey(g => g.GameId);
            modelBuilder.Entity<GameGenre>()
                .HasOne(ge => ge.Genre)
                .WithMany(gg => gg.GameGenres)
                .HasForeignKey(ge => ge.GenreId);

            modelBuilder.Entity<GameUser>()
                .HasKey(gg => new { gg.GameId, gg.UserId });
            modelBuilder.Entity<GameUser>()
                .HasOne(g => g.Game)
                .WithMany(gg => gg.GameUsers)
                .HasForeignKey(g => g.GameId);
            modelBuilder.Entity<GameUser>()
                .HasOne(u => u.User)
                .WithMany(gu => gu.UserGames)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<GamePlatform>()
                .HasKey(gp => new { gp.GameId, gp.PlatformId });
            modelBuilder.Entity<GamePlatform>()
                .HasOne(g => g.Game)
                .WithMany(gp => gp.GamePlatforms)
                .HasForeignKey(g => g.GameId);
            modelBuilder.Entity<GamePlatform>()
                .HasOne(p => p.Platform)
                .WithMany(gp => gp.GamePlatforms)
                .HasForeignKey(p => p.PlatformId);
        }
    }
}
