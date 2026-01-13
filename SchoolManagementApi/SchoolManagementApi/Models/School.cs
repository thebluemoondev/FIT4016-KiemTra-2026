using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApi.Models
{
    public class School
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        public string Principal { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
