using Space.Models;

namespace Space.Services
{
    public interface IStarService
    {
        public Task<List<Star>> GetAllAsync();
        public Task<Star?> GetByIdAsync(int id);
        public Task<List<Planet>> GetPlanetsByIdAsync(int id);
        public Task CreateAsync(Star star);
    }
}