using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationSystemDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddExamIdToStudentAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "StudentAnswer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Choices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswer_ExamId",
                table: "StudentAnswer",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_ExamId",
                table: "Choices",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Exams_ExamId",
                table: "Choices",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswer_Exams_ExamId",
                table: "StudentAnswer",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Exams_ExamId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswer_Exams_ExamId",
                table: "StudentAnswer");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswer_ExamId",
                table: "StudentAnswer");

            migrationBuilder.DropIndex(
                name: "IX_Choices_ExamId",
                table: "Choices");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "StudentAnswer");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "Choices");
        }
    }
}
