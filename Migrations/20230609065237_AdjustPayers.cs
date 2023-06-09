using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MineralWaterMonitoring.Migrations
{
    /// <inheritdoc />
    public partial class AdjustPayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Collections",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Collections");
        }
    }
}
