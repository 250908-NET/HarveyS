using School.Models;

namespace School.Services
{
    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllAsync();
        Task<Instructor?> GetByIdAsync();
        Task CreateAsync(Instructor instructor);
    }
}