using Space.Models;

namespace Space.Services
{
    public interface IMoonService
    {
        public Task<List<Moon>> GetAllAsync();
        public Task<Moon?> GetByIdAsync(int id);
        public Task<Planet?> GetPlanetByIdAsync(int id);
        public Task<Moon> CreateAsync(Moon moon);
        public Task UpdateAsync(int id, Moon moon);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
    }
}