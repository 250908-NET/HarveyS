using Space.Models;
using Space.Data;
using Microsoft.EntityFrameworkCore;

namespace Space.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly SpaceDbContext _context;

        public PlanetRepository(SpaceDbContext context)
        {
            _context = context;
        }

        public Task<List<Planet>> GetAllAsync()
        {
           throw new NotImplementedException();
        }

        public Task<Planet?> GetByIdAsync(int id)
        {
           throw new NotImplementedException();
        }

        public Task AddAsync(Planet planet)
        {
           throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
           throw new NotImplementedException();
        }
    }
}
