using Space.Models;
using Space.Data;
using Microsoft.EntityFrameworkCore;

namespace Space.Repositories
{
    public class StarRepository : IStarRepository
    {
        private readonly SpaceDbContext _context;

        public StarRepository(SpaceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Star>> GetAllAsync()
        {
            List<Star> stars = await _context.Stars.Include(e => e.Planets).ToListAsync();
            return stars;
        }

        public async Task<Star?> GetByIdAsync(int id) => await _context.Stars.FirstOrDefaultAsync(e => e.StarId == id);
        
        public async Task<List<Planet>> GetPlanetsByIdAsync(int id)
        {
            Star thisStar = await _context.Stars.FirstOrDefaultAsync(e => e.StarId == id);
            return thisStar.Planets;
        }
        public async Task<Star> AddAsync(Star star)
        {
            _context.Stars.Add(star);
            await _context.SaveChangesAsync();
            return star;
        }

        public async Task UpdateAsync(int id, Star star) 
        {
            _context.Stars.Update(star);
            await _context.SaveChangesAsync();
        }
    
        public async Task DeleteAsync(int id) 
        {
            var star = await _context.Stars.FindAsync(id);
            _context.Stars.Remove(star);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id) => await _context.Stars.AnyAsync(e => e.StarId == id);
    }
}
