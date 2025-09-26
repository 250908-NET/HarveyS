using Space.Models;

namespace Space.Repositories
{
    public interface IStarRepository
    {
        public Task<List<Star>> GetAllAsync();
        public Task<Star?> GetByIdAsync(int id);
        public Task<List<Planet>> GetPlanetsByIdAsync(int id);
        public Task<Star> AddAsync(Star star);
        public Task UpdateAsync(int id, Star star);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
    }
}