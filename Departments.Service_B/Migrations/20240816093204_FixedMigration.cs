using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Departments.Service_B.Migrations
{
    /// <inheritdoc />
    public partial class FixedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentEntityId",
                table: "Departments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentEntityId",
                table: "Departments",
                column: "DepartmentEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Departments_DepartmentEntityId",
                table: "Departments",
                column: "DepartmentEntityId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Departments_DepartmentEntityId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentEntityId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentEntityId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Department_Departments_DepartmentEntityId",
                        column: x => x.DepartmentEntityId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentEntityId",
                table: "Department",
                column: "DepartmentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentId",
                table: "Department",
                column: "DepartmentId");
        }
    }
}
