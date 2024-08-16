using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Departments.Service_B.Migrations
{
    /// <inheritdoc />
    public partial class FixedMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentId",
                table: "Departments",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Departments_ParentId",
                table: "Departments",
                column: "ParentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Departments_ParentId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ParentId",
                table: "Departments");

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
    }
}
