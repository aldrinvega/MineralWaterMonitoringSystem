using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MineralWaterMonitoring.Migrations
{
    /// <inheritdoc />
    public partial class AdjjustUsersandPayorsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupsId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Payers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupsId",
                table: "Users",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupsId",
                table: "Users",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Payers");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupId",
                table: "Users",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
