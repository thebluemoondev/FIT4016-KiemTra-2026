using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Principal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "Address", "CreatedAt", "Name", "Principal", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Trinh Van Bo, Hanoi", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "FPT Polytechnic", "Vu Chi Thanh", null },
                    { 2, "Times City, Hanoi", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "Vinschool", "Phan Ha Thuy", null },
                    { 3, "Royal City, Hanoi", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "British Vietnamese International School", "Milan Gratford", null },
                    { 4, "Hoang Minh Giam, Hanoi", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "Hanoi - Amsterdam", "Tran Thuy Duong", null },
                    { 5, "Nguyen Trai, HCM City", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "Le Hong Phong High School", "Pham Thi Be Hien", null },
                    { 6, "My Dinh, Hanoi", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "Doan Thi Diem Primary School", "Nguyen Thi Hien", null },
                    { 7, "Tran Van Bo, Hanoi", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "Marie Curie School", "Nguyen Xuan Khang", null },
                    { 8, "Yen Hoa, Hanoi", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Sieu School", "Nguyen Trong Vinh", null },
                    { 9, "My Dinh 2, Hanoi", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "Lomonoxop School", "Nguyen Phuc Chinh", null },
                    { 10, "Sala, HCM City", new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "VAS - Vietnam Australia School", "Marcel van Miert", null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "Phone", "SchoolId", "StudentId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "anhnv@fpt.edu.vn", "Nguyen Van Anh", "0912345678", 1, "PH12345", null },
                    { 2, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "binhtt@fpt.edu.vn", "Tran Thi Binh", "0922345678", 1, "PH12346", null },
                    { 3, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "chunglh@vinschool.edu.vn", "Le Huy Chung", "0932345678", 2, "VIN001", null },
                    { 4, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "dungpm@vinschool.edu.vn", "Pham My Dung", "0942345678", 2, "VIN002", null },
                    { 5, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "bachhg@bvis.edu.vn", "Hoang Gia Bach", "0952345678", 3, "BVIS01", null },
                    { 6, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "ducvm@bvis.edu.vn", "Vu Minh Duc", "0962345678", 3, "BVIS02", null },
                    { 7, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "datdt@ams.edu.vn", "Do Thanh Dat", "0972345678", 4, "AMS101", null },
                    { 8, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "linhnp@ams.edu.vn", "Ngo Phuong Linh", "0982345678", 4, "AMS102", null },
                    { 9, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "hungdv@lhp.edu.vn", "Dang Van Hung", "0992345678", 5, "LHP201", null },
                    { 10, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "kienbx@lhp.edu.vn", "Bui Xuan Kien", "0812345678", 5, "LHP202", null },
                    { 11, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "namlh@dtd.edu.vn", "Ly Hai Nam", "0822345678", 6, "DTD301", null },
                    { 12, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "khanhpq@dtd.edu.vn", "Phan Quoc Khanh", "0832345678", 6, "DTD302", null },
                    { 13, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "sontc@mc.edu.vn", "Trinh Cong Son", "0842345678", 7, "MC401", null },
                    { 14, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "giapvn@mc.edu.vn", "Vo Nguyen Giap", "0852345678", 7, "MC402", null },
                    { 15, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "sonct@ns.edu.vn", "Cao Thai Son", "0862345678", 8, "NS501", null },
                    { 16, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "thuymp@ns.edu.vn", "Mai Phuong Thuy", "0872345678", 8, "NS502", null },
                    { 17, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "tuanha@lom.edu.vn", "Ha Anh Tuan", "0882345678", 9, "LOM601", null },
                    { 18, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "nhungnh@lom.edu.vn", "Nguyen Hong Nhung", "0892345678", 9, "LOM602", null },
                    { 19, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "quyenl@vas.edu.vn", "Le Quyen", "0772345678", 10, "VAS701", null },
                    { 20, new DateTime(2026, 1, 13, 15, 0, 0, 0, DateTimeKind.Unspecified), "tungst@vas.edu.vn", "Son Tung MTP", "0782345678", 10, "VAS702", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schools_Name",
                table: "Schools",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId",
                table: "Students",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentId",
                table: "Students",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
