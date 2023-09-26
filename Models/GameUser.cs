namespace ReviewApp.Models
{
    public class GameUser
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
        public int PlayTime { get; set; }
        public int Achievements { get; set; }
    }
}
