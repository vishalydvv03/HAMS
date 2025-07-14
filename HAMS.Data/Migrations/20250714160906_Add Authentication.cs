using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptartmentId",
                table: "Doctors");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DOB",
                table: "Patients",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "DeptartmentId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 13, 14, 11, 49, 924, DateTimeKind.Utc).AddTicks(732));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 13, 14, 11, 49, 924, DateTimeKind.Utc).AddTicks(734));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 13, 14, 11, 49, 924, DateTimeKind.Utc).AddTicks(735));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 13, 14, 11, 49, 984, DateTimeKind.Utc).AddTicks(5736), "AQAAAAIAAYagAAAAEGt4OHsTfv4s31heSnsJpHchMw2NVo+TNRIQX+GaZyWYDLJwE7FkFTPBN5d9WmLPsA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 13, 14, 11, 50, 48, DateTimeKind.Utc).AddTicks(7425), "AQAAAAIAAYagAAAAEOqbTLceKnrOUJOch+JXK4dYfnAeUgKKhafhATHjGMViBNEsJazAP5phoz8o0RGcjA==" });
        }
    }
}
