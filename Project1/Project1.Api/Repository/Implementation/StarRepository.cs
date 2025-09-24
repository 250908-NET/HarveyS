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

        public Task<List<Star>> GetAllAsync()
        {
           throw new NotImplementedException();
        }

        public Task<Star?> GetByIdAsync(int id)
        {
           throw new NotImplementedException();
        }
        
        public async Task<List<Planet>> GetPlanetsByIdAsync(int id)
        {
            //return await _context.Planets.Where( planets => star.id  == id);
            throw new NotImplementedException();
        }

        public Task AddAsync(Star star)
      {
         throw new NotImplementedException();
      }

        public Task SaveChangesAsync()
        {
           throw new NotImplementedException();
        }
    }
}
