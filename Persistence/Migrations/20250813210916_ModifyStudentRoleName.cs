using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationSystemDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyStudentRoleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6861f381-5cb0-4108-8cbb-9ad9081cb63f",
                column: "Name",
                value: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6861f381-5cb0-4108-8cbb-9ad9081cb63f",
                column: "Name",
                value: "Instructor");
        }
    }
}
