using Space.Models;
using Space.Data;
using Microsoft.EntityFrameworkCore;

namespace Space.Repositories
{
    public class MoonRepository : IMoonRepository
    {
        private readonly SpaceDbContext _context;

        public MoonRepository(SpaceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Moon>> GetAllAsync()
        {
           return await _context.Moons.ToListAsync();
        }

        public async Task<Moon?> GetByIdAsync(int id)
        {
            //return await _context.Moons.Where( moon => moon.id  == id);
            throw new NotImplementedException();
        }

        public async Task<Planet?> GetPlanetByIdAsync(int id)
        {
            //return await _context.Moons.Where( planet => moon.id  == id);
            throw new NotImplementedException();
        }

        public async Task AddAsync(Moon moon)
        {
            await _context.Moons.AddAsync(moon);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
