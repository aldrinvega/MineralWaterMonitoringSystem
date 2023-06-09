using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MineralWaterMonitoring.Migrations
{
    /// <inheritdoc />
    public partial class AddjustContribution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CollectionAmount",
                table: "Contributions",
                newName: "ContributionAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContributionAmount",
                table: "Contributions",
                newName: "CollectionAmount");
        }
    }
}
