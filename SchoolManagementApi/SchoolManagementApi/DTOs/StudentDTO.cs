using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApi.DTOs
{
    public class StudentRequestDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be 2-100 characters")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Student ID is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Student ID must be 5-20 characters")]
        public string StudentId { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Phone must be 10-11 digits")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "School ID is required")]
        public int SchoolId { get; set; }
    }
}