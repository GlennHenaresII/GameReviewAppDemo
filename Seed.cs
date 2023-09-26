using ReviewApp.Data;
using ReviewApp.Models;

namespace ReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            var gameUsers = new List<GameUser>()
            {
                new GameUser()
                {
                    PlayTime = 10000,
                    Achievements = 0,
                    Game = new Game()
                    {
                        Name = "The Elder Scrolls: Skyrim",
                        ReleaseDate = new DateTime(2011,11,11),
                        Price = 60,
                        Publisher = new Publisher ()
                        {
                            Name = "Bethesda"
                        },
                        Description = "It's a game all right",
                        GameGenres = new List<GameGenre>()
                        {
                            new GameGenre { Genre = new Genre() { Name = "RPG"}},
                            new GameGenre { Genre = new Genre() { Name = "Adventure"}},
                            new GameGenre { Genre = new Genre() { Name = "First Person"}},
                            new GameGenre { Genre = new Genre() { Name = "Fantasy"}},
                            new GameGenre { Genre = new Genre() { Name = "Action"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { ReviewBody = "10/10 would get killed by giants again", Score = 8,
                            User = new User(){ Name ="Jim Skyrim", JoinDate = new DateTime(2011,11,11) } },
                            new Review { ReviewBody = "not enough Nords", Score = 7,
                            User = new User(){ Name ="Stacy Fantasy", JoinDate = new DateTime(2011,11,11) } },
                        },
                        GamePlatforms = new List<GamePlatform>()
                        {
                            new GamePlatform { Platform = new Platform() {Name = "PC"}},
                            new GamePlatform { Platform = new Platform() {Name = "PS4"}},
                            new GamePlatform { Platform = new Platform() {Name = "Xbox"}},
                        }
                    },
                    User = new User()
                    {
                        Name = "John Gamer"
                    }
                },
                new GameUser()
                {
                    PlayTime = 3222,
                    Achievements = 55,
                    Game = new Game()
                    {
                        Name = "The Elder Scrolls: Oblivion",
                        ReleaseDate = new DateTime(2006,3,20),
                        Price = 40,
                        Publisher = new Publisher ()
                        {
                            Name = "Bethesda"
                        },
                        Description = "It's a game all right 2",
                        GameGenres = new List<GameGenre>()
                        {
                            new GameGenre { Genre = new Genre() { Name = "RPG"}},
                            new GameGenre { Genre = new Genre() { Name = "Adventure"}},
                            new GameGenre { Genre = new Genre() { Name = "First Person"}},
                            new GameGenre { Genre = new Genre() { Name = "Fantasy"}},
                            new GameGenre { Genre = new Genre() { Name = "Action"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { ReviewBody = "Stop right there, criminal scum", Score = 5,
                            User = new User(){ Name ="Ron Oblivion", JoinDate = new DateTime(2011,11,11) } },
                            new Review { ReviewBody = "your goods are now forfeit", Score = 9,
                            User = new User(){ Name ="Pearson Person", JoinDate = new DateTime(2011,11,11) } },
                        },
                        GamePlatforms = new List<GamePlatform>()
                        {
                            new GamePlatform { Platform = new Platform() {Name = "PC"}},
                            new GamePlatform { Platform = new Platform() {Name = "PS4"}},
                            new GamePlatform { Platform = new Platform() {Name = "Xbox"}},
                        }
                    },
                    User = new User()
                    {
                        Name = "James Games", JoinDate = new DateTime(2011,11,11)
                    }
                },
                new GameUser()
                {
                    PlayTime = 123144,
                    Achievements = 1,
                    Game = new Game()
                    {
                        Name = "Assassin's Creed",
                        ReleaseDate = new DateTime(2007,11,13),
                        Price = 70,
                        Publisher = new Publisher ()
                        {
                            Name = "Ubisoft"
                        },
                        Description = "Be all sneakiy like",
                        GameGenres = new List<GameGenre>()
                        {
                            new GameGenre { Genre = new Genre() { Name = "Adventure"}},
                            new GameGenre { Genre = new Genre() { Name = "Third Person"}},
                            new GameGenre { Genre = new Genre() { Name = "Stealth"}},
                            new GameGenre { Genre = new Genre() { Name = "Open World"}}
                        },
                        Reviews = new List<Review>()
                        {
                            new Review { ReviewBody = "hay bales ftw", Score = 10,
                            User = new User(){ Name ="Alvin Assassin", JoinDate = new DateTime(2011,11,11) } },
                            new Review { ReviewBody = "hidden blade not hidden enough", Score = 7,
                            User = new User(){ Name ="Beth Stealth", JoinDate = new DateTime(2011,11,11) } },
                        },
                        GamePlatforms = new List<GamePlatform>()
                        {
                            new GamePlatform { Platform = new Platform() {Name = "PC"}},
                            new GamePlatform { Platform = new Platform() {Name = "PS4"}},
                            new GamePlatform { Platform = new Platform() {Name = "Xbox"}},
                        }
                    },
                    User = new User()
                    {
                        Name = "Hideo Video"
                    }
                },
            };
            dataContext.GameUsers.AddRange(gameUsers);
            dataContext.SaveChanges();
        }
    }
}
