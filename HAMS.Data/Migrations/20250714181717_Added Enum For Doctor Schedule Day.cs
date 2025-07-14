using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedEnumForDoctorScheduleDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 18, 17, 17, 234, DateTimeKind.Utc).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 18, 17, 17, 234, DateTimeKind.Utc).AddTicks(1500));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 14, 18, 17, 17, 234, DateTimeKind.Utc).AddTicks(1501));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 14, 18, 17, 17, 290, DateTimeKind.Utc).AddTicks(9003), "AQAAAAIAAYagAAAAEBdcBGnOKC3w6NJFZFigr7RlOEPFygAQZs5NuYLoFOzCeLLyy+npmisOLZjjsHcfjQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 14, 18, 17, 17, 352, DateTimeKind.Utc).AddTicks(3757), "AQAAAAIAAYagAAAAEHclz3ls6eRi+ic/dyi4jKEoz3VxWBXwoAVZNhvkGXIhp+HW0DQXBG3LErVlceeOVw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
