namespace ReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        public User User { get; set; }
        public int Score { get; set;}
        public string? ReviewBody { get; set;}
    }
}
