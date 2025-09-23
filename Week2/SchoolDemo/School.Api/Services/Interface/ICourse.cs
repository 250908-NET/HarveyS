using School.Models;

namespace School.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync();
        Task CreateAsync(Course course);
    }
}