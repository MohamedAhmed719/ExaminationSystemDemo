using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationSystemDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConvertDegreeColumnAsNullableColumnStudentCoursesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinDegree",
                table: "StudentCourse");

            migrationBuilder.AlterColumn<int>(
                name: "Degree",
                table: "StudentCourse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Degree",
                table: "StudentCourse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinDegree",
                table: "StudentCourse",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
