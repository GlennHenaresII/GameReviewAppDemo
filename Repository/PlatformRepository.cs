using ReviewApp.Data;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly DataContext _context;

        public PlatformRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePlatform(Platform platform)
        {
            _context.Add(platform);
            return Save();
        }
        public bool UpdatePlatform(Platform platform)
        {
            _context.Update(platform);
            return Save();
        }

        public bool DeletePlatform(Platform platform)
        {
            _context.Remove(platform);
            return Save();
        }

        public ICollection<Game> GetGamesByPlatform(int platformId)
        {
            return _context.GamePlatforms.Where(gp => gp.PlatformId == platformId).Select(gp => gp.Game).ToList();
        }

        public Platform GetPlatform(int platformId)
        {
            return _context.Platforms.Where(p => p.Id == platformId).FirstOrDefault();
        }

        public ICollection<Platform> GetPlatforms()
        {
            return _context.Platforms.OrderBy(p => p.Id).ToList();
        }

        public bool PlatformExists(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
