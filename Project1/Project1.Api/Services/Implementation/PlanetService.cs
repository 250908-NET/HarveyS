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

        public async Task CreateAsync(Planet planet)
        {
            await _repo.AddAsync(planet);
            await _repo.SaveChangesAsync();
        }
    }
}