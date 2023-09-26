namespace ReviewApp.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GamePlatform> GamePlatforms { get; set; }
    }
}
