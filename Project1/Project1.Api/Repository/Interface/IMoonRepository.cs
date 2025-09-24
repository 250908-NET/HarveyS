using Space.Models;

namespace Space.Repositories
{
    public interface IMoonRepository
    {
        public Task<List<Moon>> GetAllAsync();
        public Task<Moon?> GetByIdAsync(int id);
        public Task AddAsync(Moon moon);
        public Task SaveChangesAsync();
    }
}