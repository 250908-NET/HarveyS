using Space.Models;

namespace Space.Repositories
{
    public interface IStarRepository
    {
        public Task<List<Star>> GetAllAsync();
        public Task<Star?> GetByIdAsync(int id);
        public Task AddAsync(Star star);
        public Task SaveChangesAsync();
    }
}