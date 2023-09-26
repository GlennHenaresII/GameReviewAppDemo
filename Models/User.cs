using Azure.Core.Pipeline;

namespace ReviewApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }
        public ICollection<Review>? UserReviews { get; set;}
        public ICollection<GameUser>? UserGames { get; set; }
    }
}
