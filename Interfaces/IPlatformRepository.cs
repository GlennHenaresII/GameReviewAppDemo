using Microsoft.EntityFrameworkCore;
using ReviewApp.Models;

namespace ReviewApp.Interfaces
{
    public interface IPlatformRepository
    {
        ICollection<Platform> GetPlatforms();
        Platform GetPlatform(int platformId);
        bool PlatformExists(int platformId);
        ICollection<Game> GetGamesByPlatform(int platformId);
        bool CreatePlatform(Platform platform);
        bool UpdatePlatform(Platform platform);
        bool DeletePlatform(Platform platform);
        bool Save();
    }
}
