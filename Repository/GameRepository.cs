using Microsoft.EntityFrameworkCore;
using ReviewApp.Data;
using ReviewApp.Interfaces;
using ReviewApp.Models;
using System;
using System.Linq;

namespace ReviewApp.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;

        public GameRepository(DataContext context) 
        { 
            _context = context;
        }

        public bool CreateGame(int[] genreIds, int[] platformIds, Game game)
        {
            var genres = _context.Genres.Where(ge => genreIds.Contains(ge.Id)).ToList();
            var platforms = _context.Platforms.Where(p => platformIds.Contains(p.Id)).ToList();

            foreach (var genre in genres)
            {
                var gameGenre = new GameGenre()
                {
                    Genre = genre,
                    Game = game,
                };

                _context.Add(gameGenre);
            }

            foreach (var platform in platforms)
            {
                var gamePlatform = new GamePlatform
                {
                    Platform = platform,
                    Game = game,
                };

                _context.Add(gamePlatform);
            }

            _context.Add(game);

            return Save();
        }

        public bool UpdateGame(int[] genreIds, int[] platformIds, Game game)
        {
            _context.Update(game);
            return Save();
        }

        public bool DeleteGame(Game game)
        {
            _context.Remove(game);
            return Save();
        }

        public Game GetGame(int gameId)
        {
            return _context.Games.Where(g => g.Id == gameId).Include(g => g.Publisher).FirstOrDefault();
        }

        public Game GetGame(string name)
        {
            return _context.Games.Where(g => g.Name == name).FirstOrDefault();
        }

        public decimal GetGameRating(int gameId)
        {
            return _context.Games.Where(g => g.Id == gameId).SelectMany(g => g.Reviews).DefaultIfEmpty().Average(g => (decimal?)g.Score ?? 0);
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(g => g.Id).Include(g => g.Publisher).ToList();
        }

        public bool GameExists(int gameId)
        {
            return _context.Games.Any(g => g.Id == gameId);
        }

        public int GetPlayerCount(int gameId)
        {
            return _context.GameUsers.Where(g => g.Game.Id == gameId).Count();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }
    }
}
