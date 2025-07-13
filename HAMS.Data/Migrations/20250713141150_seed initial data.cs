using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedinitialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departements_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departements",
                table: "Departements");

            migrationBuilder.RenameTable(
                name: "Departements",
                newName: "Departments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "DepartmentId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departments_DepartmentId",
                table: "Doctors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DepartmentId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Departements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departements",
                table: "Departements",
                column: "DepartmentId");

            migrationBuilder.UpdateData(
                table: "Departements",
                keyColumn: "DepartmentId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 13, 10, 12, 43, 685, DateTimeKind.Utc).AddTicks(9623));

            migrationBuilder.UpdateData(
                table: "Departements",
                keyColumn: "DepartmentId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 13, 10, 12, 43, 685, DateTimeKind.Utc).AddTicks(9626));

            migrationBuilder.UpdateData(
                table: "Departements",
                keyColumn: "DepartmentId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 13, 10, 12, 43, 685, DateTimeKind.Utc).AddTicks(9627));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 13, 10, 12, 43, 748, DateTimeKind.Utc).AddTicks(7), "AQAAAAIAAYagAAAAEPHbQloKuhFHrMmQ+KuEUt7N6Aq4zbHLBwxHTylp0rTlnqrwSgycbekYT6Jl1hmkOg==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 13, 10, 12, 43, 807, DateTimeKind.Utc).AddTicks(8599), "AQAAAAIAAYagAAAAEBBYciJ1fksdpDt6OZJuh20HpUSt+8V+wlK1BmQ25esmwOq5YANubD6WMdTylNTQdA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departements_DepartmentId",
                table: "Doctors",
                column: "DepartmentId",
                principalTable: "Departements",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
