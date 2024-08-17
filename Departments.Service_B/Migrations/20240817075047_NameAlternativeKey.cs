using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Departments.Service_B.Migrations
{
    /// <inheritdoc />
    public partial class NameAlternativeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Departments_Name",
                table: "Departments",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Departments_Name",
                table: "Departments");
        }
    }
}
