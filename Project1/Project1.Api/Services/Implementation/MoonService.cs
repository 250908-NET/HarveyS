using Space.Models;
using Space.Repositories;

namespace Space.Services
{
    public class MoonService : IMoonService
    {
        private readonly IMoonRepository _repo;

        public MoonService(IMoonRepository repo)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            _repo = repo;
        }

        public async Task<List<Moon>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Moon?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);


        public async Task<Planet?> GetPlanetByIdAsync(int id) => await _repo.GetPlanetByIdAsync(id);


        public async Task<Moon> CreateAsync(Moon moon)
        {
            Moon createdMoon = await _repo.AddAsync(moon);
            return createdMoon;
        }

        public async Task UpdateAsync(int id, Moon moon)
        {
            await _repo.UpdateAsync(id, moon);
        }
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
        
        public async Task<bool> Exists(int id) => await _repo.Exists(id);

    }
}