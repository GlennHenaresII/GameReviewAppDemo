using ReviewApp.Models;

namespace ReviewApp.Interfaces
{
    public interface IGameRepository
    {
        ICollection<Game> GetGames();
        Game GetGame(int gameId);
        Game GetGame(string gameName);
        decimal GetGameRating(int gameId);
        bool GameExists(int gameId);
        int GetPlayerCount(int gameId);
        bool CreateGame(int[] genreId, int[] platformId, Game game);
        bool UpdateGame(int[] genreId, int[] platformId, Game game);
        bool DeleteGame(Game game);
        bool Save();
    }
}
