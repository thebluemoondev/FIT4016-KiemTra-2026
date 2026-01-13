using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementApi.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SchoolId { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string StudentId { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("SchoolId")]
        public virtual School School { get; set; } = null!;
    }
}
