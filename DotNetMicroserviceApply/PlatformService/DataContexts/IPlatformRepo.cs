using PlatformService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlatformService.DataContexts
{
    public interface IPlatformRepo
    {
        Task<bool> SaveChanges();

        IEnumerable<Platform> GetAllPlatform();

        Task<Platform> GetPlatformById(int id);

        void CreatePlatform(Platform platform);
    }
}
