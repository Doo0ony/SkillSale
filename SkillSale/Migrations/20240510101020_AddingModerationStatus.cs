using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSale.Migrations
{
    /// <inheritdoc />
    public partial class AddingModerationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModerationStatus",
                table: "Vacancies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvaliable",
                table: "Resumes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModerationStatus",
                table: "Resumes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerationStatus",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "IsAvaliable",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "ModerationStatus",
                table: "Resumes");
        }
    }
}
