using ReviewApp.Models;

namespace ReviewApp.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Price { get; set; }
        public string PublisherName { get; set; }
    }
}
