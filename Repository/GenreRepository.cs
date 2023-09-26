using ReviewApp.Data;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGenre(Genre genre)
        {
            _context.Add(genre);
            return Save();
        }

        public bool UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            return Save();
        }

        public bool DeleteGenre(Genre genre)
        {
            _context.Remove(genre);
            return Save();
        }

        public bool GenreExists(int genreId)
        {
            return _context.Genres.Any(ge => ge.Id == genreId);
        }

        public ICollection<Game> GetGamesByGenre(int genreId)
        {
            return _context.GameGenres.Where(gg => gg.GenreId == genreId).Select(gg => gg.Game).ToList();
        }
        public ICollection<Genre> GetGenresByGame(int gameId)
        {
            return _context.GameGenres.Where(gg => gg.GameId == gameId).Select(gg => gg.Genre).ToList();
        }

        public Genre GetGenre(int genreId)
        {
            return _context.Genres.Where(ge => ge.Id == genreId).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.OrderBy(ge => ge.Id).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
