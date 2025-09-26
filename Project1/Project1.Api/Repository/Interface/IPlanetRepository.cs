using Space.Models;

namespace Space.Repositories
{
    public interface IPlanetRepository
    {
        public Task<List<Planet>> GetAllAsync();
        public Task<Planet?> GetByIdAsync(int id);
        public Task<List<Moon>> GetMoonsByIdAsync(int id);
        public Task<List<Star>> GetStarsByIdAsync(int id);
        public Task<Planet> AddAsync(Planet planet);
        public Task UpdateAsync(int id, Planet planet);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
    }
}