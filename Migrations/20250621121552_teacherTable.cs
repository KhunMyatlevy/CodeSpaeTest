using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiApp.Migrations
{
    /// <inheritdoc />
    public partial class teacherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "subject",
                table: "Teachers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subject",
                table: "Teachers");
        }
    }
}
