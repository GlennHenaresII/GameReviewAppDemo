using ReviewApp.Models;

namespace ReviewApp.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        Genre GetGenre(int genreId);
        ICollection<Game> GetGamesByGenre(int genreId);
        ICollection<Genre> GetGenresByGame(int gameId);
        bool GenreExists(int genreId);
        bool CreateGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Genre genre);
        bool Save();
    }
}
