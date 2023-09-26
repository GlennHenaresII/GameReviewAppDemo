using ReviewApp.Models;

namespace ReviewApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        User GetUser(int userId);
        bool UserExists(int userId);
        bool UserOwnsGame(int userId, int gameId);
        ICollection<Game> GetUserGames(int userId);
        ICollection<Review> GetUserReviews(int userId);
        int GetUserPlaytimeByGame(int userId, int gameId);
        int GetUserAchievementsByGame(int userId, int gameId);
        bool AddUserGame(int userId, int gameId);
        bool Save();
    }
}
