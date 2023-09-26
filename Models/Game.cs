namespace ReviewApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Price { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; }
        public ICollection<GameGenre>? GameGenres { get; set; }
        public ICollection<GameUser>? GameUsers { get; set; }
    }
}
