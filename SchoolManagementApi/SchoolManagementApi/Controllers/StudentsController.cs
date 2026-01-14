using Microsoft.AspNetCore.Mvc;
using SchoolManagementApi.DTOs;
using SchoolManagementApi.Models;
using SchoolManagementApi.Repositories;

namespace SchoolManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repo;

        public StudentsController(IStudentRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _repo.GetAllStudentsAsync();
            return Ok(students);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> Create(StudentRequestDto dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                StudentId = dto.StudentId,
                Email = dto.Email,
                Phone = dto.Phone,
                SchoolId = dto.SchoolId,
                CreatedAt = DateTime.Now
            };

            await _repo.CreateStudentAsync(student);
            await _repo.SaveChangesAsync();

            return Ok(new { message = "Student created successfully" });
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StudentRequestDto dto)
        {
            var student = await _repo.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            student.FullName = dto.FullName;
            student.StudentId = dto.StudentId; // Cập nhật cả StudentId nếu cần
            student.Email = dto.Email;
            student.Phone = dto.Phone;
            student.SchoolId = dto.SchoolId;
            student.UpdatedAt = DateTime.Now;

            _repo.UpdateStudent(student);
            await _repo.SaveChangesAsync();

            return Ok(new { message = "Student updated successfully" });
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteStudentAsync(id);
            if (success)
            {
                await _repo.SaveChangesAsync();
                return Ok(new { message = "Student deleted successfully" });
            }

            return NotFound(new { message = "Student not found" });
        }
    }
}