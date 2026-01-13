using Microsoft.EntityFrameworkCore;
using SchoolManagementApi.Data;
using SchoolManagementApi.Models;

namespace SchoolManagementApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        // 1. Lấy toàn bộ danh sách (Không phân trang ở Server)
        public async Task<IEnumerable<object>> GetAllStudentsAsync()
        {
            return await _context.Students
                .Include(s => s.School) // Eager loading để lấy tên trường
                .OrderByDescending(s => s.Id)
                .Select(s => new {
                    s.Id,
                    s.FullName,
                    s.StudentId,
                    s.Email,
                    s.Phone,
                    SchoolName = s.School != null ? s.School.Name : "N/A",
                    s.SchoolId
                })
                .ToListAsync();
        }

        // 2. Lấy thông tin 1 học sinh theo ID
        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        // 3. Đăng ký thêm mới vào Context
        public async Task CreateStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        // 4. Đánh dấu cập nhật trạng thái
        public void UpdateStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
        }

        // 5. Xóa học sinh
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            return true;
        }

        // 6. Lưu tất cả thay đổi xuống Database
        public async Task<bool> SaveChangesAsync()
        {
            // Trả về true nếu có ít nhất 1 bản ghi được cập nhật thành công
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}