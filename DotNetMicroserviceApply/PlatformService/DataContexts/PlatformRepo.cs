using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.DataContexts
{
    class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext db;
        public PlatformRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            db.Platforms.Add(platform);
            await SaveChanges();
        }

        public IEnumerable<Platform> GetAllPlatform()
        {
            return db.Platforms.ToList();
        }

        public async Task<Platform> GetPlatformById(int id)
        {
            return await db.Platforms.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> SaveChanges()
        {
            return (await db.SaveChangesAsync() >= 0);
        }
    }
}
