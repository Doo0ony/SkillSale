using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSale.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavoriteResumes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteResumes_Vacancies_ResumeId",
                table: "FavoriteResumes");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteResumes_Resumes_ResumeId",
                table: "FavoriteResumes",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteResumes_Resumes_ResumeId",
                table: "FavoriteResumes");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteResumes_Vacancies_ResumeId",
                table: "FavoriteResumes",
                column: "ResumeId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
