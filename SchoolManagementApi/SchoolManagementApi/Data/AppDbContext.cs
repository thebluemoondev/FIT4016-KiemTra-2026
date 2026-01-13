using Microsoft.EntityFrameworkCore;
using SchoolManagementApi.Models;

namespace SchoolManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Cấu hình bảng Schools
            modelBuilder.Entity<School>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Principal).IsRequired();
                entity.Property(e => e.Address).IsRequired();
            });

            // 2. Cấu hình bảng Students
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.StudentId).IsRequired();
                entity.HasIndex(e => e.StudentId).IsUnique();

                entity.HasOne(s => s.School)
                      .WithMany(sc => sc.Students)
                      .HasForeignKey(s => s.SchoolId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var staticDate = new DateTime(2026, 1, 13, 15, 0, 0);

            modelBuilder.Entity<School>().HasData(
                new School { Id = 1, Name = "FPT Polytechnic", Principal = "Vu Chi Thanh", Address = "Trinh Van Bo, Hanoi", CreatedAt = staticDate },
                new School { Id = 2, Name = "Vinschool", Principal = "Phan Ha Thuy", Address = "Times City, Hanoi", CreatedAt = staticDate },
                new School { Id = 3, Name = "British Vietnamese International School", Principal = "Milan Gratford", Address = "Royal City, Hanoi", CreatedAt = staticDate },
                new School { Id = 4, Name = "Hanoi - Amsterdam", Principal = "Tran Thuy Duong", Address = "Hoang Minh Giam, Hanoi", CreatedAt = staticDate },
                new School { Id = 5, Name = "Le Hong Phong High School", Principal = "Pham Thi Be Hien", Address = "Nguyen Trai, HCM City", CreatedAt = staticDate },
                new School { Id = 6, Name = "Doan Thi Diem Primary School", Principal = "Nguyen Thi Hien", Address = "My Dinh, Hanoi", CreatedAt = staticDate },
                new School { Id = 7, Name = "Marie Curie School", Principal = "Nguyen Xuan Khang", Address = "Tran Van Bo, Hanoi", CreatedAt = staticDate },
                new School { Id = 8, Name = "Nguyen Sieu School", Principal = "Nguyen Trong Vinh", Address = "Yen Hoa, Hanoi", CreatedAt = staticDate },
                new School { Id = 9, Name = "Lomonoxop School", Principal = "Nguyen Phuc Chinh", Address = "My Dinh 2, Hanoi", CreatedAt = staticDate },
                new School { Id = 10, Name = "VAS - Vietnam Australia School", Principal = "Marcel van Miert", Address = "Sala, HCM City", CreatedAt = staticDate }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, SchoolId = 1, FullName = "Nguyen Van Anh", StudentId = "PH12345", Email = "anhnv@fpt.edu.vn", Phone = "0912345678", CreatedAt = staticDate },
                new Student { Id = 2, SchoolId = 1, FullName = "Tran Thi Binh", StudentId = "PH12346", Email = "binhtt@fpt.edu.vn", Phone = "0922345678", CreatedAt = staticDate },
                new Student { Id = 3, SchoolId = 2, FullName = "Le Huy Chung", StudentId = "VIN001", Email = "chunglh@vinschool.edu.vn", Phone = "0932345678", CreatedAt = staticDate },
                new Student { Id = 4, SchoolId = 2, FullName = "Pham My Dung", StudentId = "VIN002", Email = "dungpm@vinschool.edu.vn", Phone = "0942345678", CreatedAt = staticDate },
                new Student { Id = 5, SchoolId = 3, FullName = "Hoang Gia Bach", StudentId = "BVIS01", Email = "bachhg@bvis.edu.vn", Phone = "0952345678", CreatedAt = staticDate },
                new Student { Id = 6, SchoolId = 3, FullName = "Vu Minh Duc", StudentId = "BVIS02", Email = "ducvm@bvis.edu.vn", Phone = "0962345678", CreatedAt = staticDate },
                new Student { Id = 7, SchoolId = 4, FullName = "Do Thanh Dat", StudentId = "AMS101", Email = "datdt@ams.edu.vn", Phone = "0972345678", CreatedAt = staticDate },
                new Student { Id = 8, SchoolId = 4, FullName = "Ngo Phuong Linh", StudentId = "AMS102", Email = "linhnp@ams.edu.vn", Phone = "0982345678", CreatedAt = staticDate },
                new Student { Id = 9, SchoolId = 5, FullName = "Dang Van Hung", StudentId = "LHP201", Email = "hungdv@lhp.edu.vn", Phone = "0992345678", CreatedAt = staticDate },
                new Student { Id = 10, SchoolId = 5, FullName = "Bui Xuan Kien", StudentId = "LHP202", Email = "kienbx@lhp.edu.vn", Phone = "0812345678", CreatedAt = staticDate },
                new Student { Id = 11, SchoolId = 6, FullName = "Ly Hai Nam", StudentId = "DTD301", Email = "namlh@dtd.edu.vn", Phone = "0822345678", CreatedAt = staticDate },
                new Student { Id = 12, SchoolId = 6, FullName = "Phan Quoc Khanh", StudentId = "DTD302", Email = "khanhpq@dtd.edu.vn", Phone = "0832345678", CreatedAt = staticDate },
                new Student { Id = 13, SchoolId = 7, FullName = "Trinh Cong Son", StudentId = "MC401", Email = "sontc@mc.edu.vn", Phone = "0842345678", CreatedAt = staticDate },
                new Student { Id = 14, SchoolId = 7, FullName = "Vo Nguyen Giap", StudentId = "MC402", Email = "giapvn@mc.edu.vn", Phone = "0852345678", CreatedAt = staticDate },
                new Student { Id = 15, SchoolId = 8, FullName = "Cao Thai Son", StudentId = "NS501", Email = "sonct@ns.edu.vn", Phone = "0862345678", CreatedAt = staticDate },
                new Student { Id = 16, SchoolId = 8, FullName = "Mai Phuong Thuy", StudentId = "NS502", Email = "thuymp@ns.edu.vn", Phone = "0872345678", CreatedAt = staticDate },
                new Student { Id = 17, SchoolId = 9, FullName = "Ha Anh Tuan", StudentId = "LOM601", Email = "tuanha@lom.edu.vn", Phone = "0882345678", CreatedAt = staticDate },
                new Student { Id = 18, SchoolId = 9, FullName = "Nguyen Hong Nhung", StudentId = "LOM602", Email = "nhungnh@lom.edu.vn", Phone = "0892345678", CreatedAt = staticDate },
                new Student { Id = 19, SchoolId = 10, FullName = "Le Quyen", StudentId = "VAS701", Email = "quyenl@vas.edu.vn", Phone = "0772345678", CreatedAt = staticDate },
                new Student { Id = 20, SchoolId = 10, FullName = "Son Tung MTP", StudentId = "VAS702", Email = "tungst@vas.edu.vn", Phone = "0782345678", CreatedAt = staticDate }
            );
        }
    }
}