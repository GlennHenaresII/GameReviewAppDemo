using Microsoft.EntityFrameworkCore;
using ReviewApp.Data;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public int GetUserAchievementsByGame(int userId, int gameId)
        {
            return _context.GameUsers.Where(gu => gu.UserId == userId && gu.GameId == gameId).Select(gu => gu.Achievements).FirstOrDefault();
        }

        public ICollection<Game> GetUserGames(int userId)
        {
            return _context.GameUsers.Where(gu => gu.UserId == userId).Select(gu => gu.Game).ToList();
        }

        public int GetUserPlaytimeByGame(int userId, int gameId)
        {
            return _context.GameUsers.Where(gu => gu.UserId == userId && gu.GameId == gameId).Select(gu => gu.PlayTime).FirstOrDefault();
        }

        public ICollection<Review> GetUserReviews(int userId)
        {
            return _context.Reviews.Where(r => r.User.Id == userId).ToList();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
        public bool UserOwnsGame(int userId, int gameId)
        {
            return _context.GameUsers.Any(gu => gu.User.Id == userId && gu.Game.Id == gameId);
        }

        public bool AddUserGame(int userId, int gameId)
        {
            var game = _context.Games.Where(g => g.Id == gameId).FirstOrDefault();
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            var gameUser = new GameUser()
            {
                Game = game,
                User = user,
                PlayTime = 1
            };
            
            _context.Add(gameUser);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
