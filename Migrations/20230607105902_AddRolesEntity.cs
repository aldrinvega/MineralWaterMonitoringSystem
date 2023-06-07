using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MineralWaterMonitoring.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RolesId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolesId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolesId",
                table: "Users",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RolesId",
                table: "Users",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "RolesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RolesId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_RolesId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RolesId",
                table: "Users");
        }
    }
}
