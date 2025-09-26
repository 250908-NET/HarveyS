using Space.Models;

namespace Space.Services
{
    public interface IStarService
    {
        public Task<List<Star>> GetAllAsync();
        public Task<Star?> GetByIdAsync(int id);
        public Task<List<Planet>> GetPlanetsByIdAsync(int id);
        public Task<Star> CreateAsync(Star star);
        public Task UpdateAsync(int id, Star star);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
    }
}