using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExaminationSystemDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentRoleAndInstructorRoleToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4da9aaf8-3b75-4c97-9fca-f637c691ba50", "6d2a15d4-e6d5-4ffd-8c7c-12099d428efc", "Instructor", "INSTRUCTOR" },
                    { "6861f381-5cb0-4108-8cbb-9ad9081cb63f", "24ebe67b-943b-46ac-b3fa-516b76e2f5fe", "Instructor", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4da9aaf8-3b75-4c97-9fca-f637c691ba50");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6861f381-5cb0-4108-8cbb-9ad9081cb63f");
        }
    }
}
