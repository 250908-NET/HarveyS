using Space.Models;

namespace Space.Repositories
{
    public interface IMoonRepository
    {
        public Task<List<Moon>> GetAllAsync();
        public Task<Moon?> GetByIdAsync(int id);
        public Task<Planet?> GetPlanetByIdAsync(int id);
        public Task<Moon> AddAsync(Moon moon);
        public Task UpdateAsync(int id, Moon moon);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
        // public Task SaveChangesAsync();
    }
}