using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorQuizApi.Api.Migrations
{
    /// <inheritdoc />
    public partial class MainQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuizName",
                table: "UserQuizResults",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuizMainId",
                table: "Questions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Quiz_Main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz_Main", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizMainId",
                table: "Questions",
                column: "QuizMainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quiz_Main_QuizMainId",
                table: "Questions",
                column: "QuizMainId",
                principalTable: "Quiz_Main",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quiz_Main_QuizMainId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Quiz_Main");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuizMainId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuizName",
                table: "UserQuizResults");

            migrationBuilder.DropColumn(
                name: "QuizMainId",
                table: "Questions");
        }
    }
}
