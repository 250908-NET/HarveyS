using Space.Models;

namespace Space.Services
{
    public interface IPlanetService
    {
        public Task<List<Planet>> GetAllAsync();
        public Task<Planet?> GetByIdAsync(int id);
        public Task CreateAsync(Planet planet);
    }
}