using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSale.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResumeTableContactFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Resumes");
        }
    }
}
