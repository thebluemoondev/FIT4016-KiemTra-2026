using SchoolManagementApi.Models;

namespace SchoolManagementApi.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<object>> GetAllStudentsAsync();

        Task<Student?> GetStudentByIdAsync(int id);
        Task CreateStudentAsync(Student student);
        void UpdateStudent(Student student);
        Task<bool> DeleteStudentAsync(int id);

        Task<bool> SaveChangesAsync();
    }
}