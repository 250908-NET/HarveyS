using Space.Models;

namespace Space.Repositories
{
    public interface IPlanetRepository
    {
        public Task<List<Planet>> GetAllAsync();
        public Task<Planet?> GetByIdAsync(int id);
        public Task<List<Moon>> GetMoonsByIdAsync(int id);
        public Task AddAsync(Planet planet);
        public Task SaveChangesAsync();
    }
}