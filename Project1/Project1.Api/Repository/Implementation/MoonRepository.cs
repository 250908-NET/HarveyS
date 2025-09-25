using Space.Models;
using Space.Data;
using Microsoft.EntityFrameworkCore;

namespace Space.Repositories
{
    public class MoonRepository : IMoonRepository
    {
        private readonly SpaceDbContext _context;

        public MoonRepository(SpaceDbContext context) => _context = context;

        public async Task<List<Moon>> GetAllAsync() => await _context.Moons.Include(e => e.Name).Include(e => e.Planet).ToListAsync();

        public async Task<Moon?> GetByIdAsync(int id) => await _context.Courses.Include(e => e.Name).Include(e => e.Planet).FirstOrDefaultAsync(e => e.Id == id);

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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
