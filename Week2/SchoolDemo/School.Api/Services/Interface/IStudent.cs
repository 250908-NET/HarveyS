using School.Models;

namespace School.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync();
        Task CreateAsync(Student student);
    }
}