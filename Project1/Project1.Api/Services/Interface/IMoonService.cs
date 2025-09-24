using Space.Models;

namespace Space.Services
{
    public interface IMoonService
    {
        public Task<List<Moon>> GetAllAsync();
        public Task<Moon?> GetByIdAsync(int id);
        public Task<Planet?> GetPlanetByIdAsync(int id);
        public Task CreateAsync(Moon moon);
    }
}