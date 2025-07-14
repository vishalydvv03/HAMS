using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class DoctorSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    IsOnLeave = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 17, 45, 24, 813, DateTimeKind.Utc).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 17, 45, 24, 813, DateTimeKind.Utc).AddTicks(5657));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 17, 45, 24, 813, DateTimeKind.Utc).AddTicks(5658));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 14, 17, 45, 24, 871, DateTimeKind.Utc).AddTicks(544), "AQAAAAIAAYagAAAAEIRpgVXM22BywVABCNhiDB53O8ZLPIgt9LA+NTnybw8JMkoUFbfuA3/+44op8Hywsw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 14, 17, 45, 24, 927, DateTimeKind.Utc).AddTicks(7792), "AQAAAAIAAYagAAAAEJCnKqQdn6+Li9bwp35dhNIMQ6BAchQ5lj5PazDVyndBU8n67/W04DFl7V5kV7mi3w==" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId",
                table: "DoctorSchedules",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 16, 9, 5, 558, DateTimeKind.Utc).AddTicks(9297));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 16, 9, 5, 558, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 16, 9, 5, 558, DateTimeKind.Utc).AddTicks(9301));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 14, 16, 9, 5, 618, DateTimeKind.Utc).AddTicks(2542), "AQAAAAIAAYagAAAAEAHEx6HNXqAZ507XAxgdcSORyPpckiuviXhgibklF5vepo6vtdzAjOJoVMXVr0TbGQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 14, 16, 9, 5, 674, DateTimeKind.Utc).AddTicks(2513), "AQAAAAIAAYagAAAAEPmV2LDv/rHR6si2iFiYC8vT7yymq9uga04GHlzbUjpe0RJV9uwg39efswCgWTHroQ==" });
        }
    }
}
