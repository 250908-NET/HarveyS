using Space.Models;
using Space.Repositories;

namespace Space.Services
{
    public class MoonService : IMoonService
    {
        private readonly IMoonRepository _repo;

        public MoonService(IMoonRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Moon>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Moon?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);


        public async Task<Planet?> GetPlanetByIdAsync(int id) => await _repo.GetPlanetByIdAsync(id);


        public async Task CreateAsync(Moon moon)
        {
            await _repo.AddAsync(moon);
            await _repo.SaveChangesAsync();
        }
    }
}