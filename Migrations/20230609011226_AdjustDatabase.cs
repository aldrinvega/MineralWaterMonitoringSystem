using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MineralWaterMonitoring.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "UserCode",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RemainingBalance",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    CollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.CollectionId);
                    table.ForeignKey(
                        name: "FK_Collections_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contributions",
                columns: table => new
                {
                    ContributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributions", x => x.ContributionId);
                    table.ForeignKey(
                        name: "FK_Contributions_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "CollectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contributions_Payers_PayerId",
                        column: x => x.PayerId,
                        principalTable: "Payers",
                        principalColumn: "PayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_GroupId",
                table: "Collections",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_CollectionId",
                table: "Contributions",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_PayerId",
                table: "Contributions",
                column: "PayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contributions");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropColumn(
                name: "RemainingBalance",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupsId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
