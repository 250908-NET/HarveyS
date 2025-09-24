using Space.Models;
using Space.Repositories;

namespace Space.Services
{
    public class StarService : IStarService
    {
        private readonly IStarRepository _repo;

        public StarService(IStarRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Star>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Star?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        
        public async Task<List<Planet>> GetPlanetsByIdAsync(int id) => await _repo.GetPlanetsByIdAsync(id);

        public async Task CreateAsync(Star star)
        {
            await _repo.AddAsync(star);
            await _repo.SaveChangesAsync();
        }
    }
}