using Space.Models;
using Space.Data;
using Microsoft.EntityFrameworkCore;

namespace Space.Repositories
{
    public class MoonRepository : IMoonRepository
    {
        private readonly SpaceDbContext _context;

        public MoonRepository(SpaceDbContext context) => _context = context;
        public async Task<List<Moon>> GetAllAsync()
        {
            List<Moon> moons = await _context.Moons.ToListAsync();
            return moons;
        }

        public async Task<Moon?> GetByIdAsync(int id) => await _context.Moons.FirstOrDefaultAsync(e => e.MoonId == id);

        public async Task<Planet?> GetPlanetByIdAsync(int id)
        {
            //return await _context.Moons.Where( planet => moon.id  == id);
            throw new NotImplementedException();
        }

        public async Task<Moon> AddAsync(Moon moon)
        {
            _context.Moons.Add(moon);
            await _context.SaveChangesAsync();
            return moon;
        }

        public async Task UpdateAsync(int id, Moon moon) 
        {
            _context.Moons.Update(moon);
            await _context.SaveChangesAsync();
        }
    
        public async Task DeleteAsync(int id) 
        {
            var moon = await _context.Moons.FindAsync(id);
            _context.Moons.Remove(moon);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id) => await _context.Moons.AnyAsync(e => e.MoonId == id);

        // public async Task SaveChangesAsync()
        // {
        //     await _context.SaveChangesAsync();
        // }
    }
}
