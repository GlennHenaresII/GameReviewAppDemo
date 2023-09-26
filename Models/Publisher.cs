namespace ReviewApp.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game>? Games { get; set; }
    }
}
