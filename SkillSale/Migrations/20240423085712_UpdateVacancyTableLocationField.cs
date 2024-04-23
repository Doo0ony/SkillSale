using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSale.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVacancyTableLocationField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "Vacancies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Vacancies");
        }
    }
}
