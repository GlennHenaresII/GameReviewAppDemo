namespace ReviewApp.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int gameId { get; set; }
        public int Score { get; set; }
        public string? ReviewBody { get; set; }
    }
}
