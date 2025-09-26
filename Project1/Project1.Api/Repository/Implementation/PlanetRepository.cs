using Space.Models;
using Space.Data;
using Microsoft.EntityFrameworkCore;

namespace Space.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly SpaceDbContext _context;

        public PlanetRepository(SpaceDbContext context) => _context = context;

        public async Task<List<Planet>> GetAllAsync()
        {
            List<Planet> planets = await _context.Planets.Include(e => e.moons).Include(e => e.stars).ToListAsync();
            return planets;
        }

        public async Task<Planet?> GetByIdAsync(int id) => await _context.Planets.FirstOrDefaultAsync(e => e.PlanetId == id);

        public async Task<List<Moon>> GetMoonsByIdAsync(int id)
        {
            List<Moon> moons = await _context.Moons.Where(e => e.planet.PlanetId == id).ToListAsync();
            return moons;
        }
        
        public async Task<List<Star>> GetStarsByIdAsync(int id)
        {
            Planet thisPlanet = await _context.Planets.FirstOrDefaultAsync(e => e.PlanetId == id);
            return thisPlanet.stars;
        }
        
        public async Task<Planet> AddAsync(Planet planet)
        {
            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();
            return planet;
        }

        public async Task UpdateAsync(int id, Planet planet) 
        {
            _context.Planets.Update(planet);
            await _context.SaveChangesAsync();
        }
    
        public async Task DeleteAsync(int id) 
        {
            var planet = await _context.Planets.FindAsync(id);
            _context.Planets.Remove(planet);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id) => await _context.Planets.AnyAsync(e => e.PlanetId == id);
    }
}
