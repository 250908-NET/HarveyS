// using School.Models;

// namespace School.Services
// {
//     public interface StudentService : IStudentService
//     {
//         private readonly IStudentRepository _repo;

//         public StudentService(IStudentRepository repo)
//         {
//             _repo = repo;
//         }

//         Task<List<Student>> GetAllAsync() => _repo.GetAllAsync();

//         Task<Student?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

//         Task CreateAsync(Student student)
//         {
//             await _repo.AddAsync(student);
//             await _repo.SaveChangesAsync();
//         }
//     }
// }