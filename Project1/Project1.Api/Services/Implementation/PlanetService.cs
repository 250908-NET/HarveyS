using Space.Models;
using Space.Repositories;

namespace Space.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository _repo;

        public PlanetService(IPlanetRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Planet>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Planet?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        
        public async Task<List<Moon>> GetMoonsByIdAsync(int id) => await _repo.GetMoonsByIdAsync(id);
        public async Task<List<Star>> GetStarsByIdAsync(int id) => await _repo.GetStarsByIdAsync(id);
    

        public async Task<Planet> CreateAsync(Planet planet)
        {
            Planet createdPlanet = await _repo.AddAsync(planet);
            return createdPlanet;
        }

        public async Task UpdateAsync(int id, Planet planet)
        {
            await _repo.UpdateAsync(id, planet);
        }
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
        
        public async Task<bool> Exists(int id) => await _repo.Exists(id);
    }
}