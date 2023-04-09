using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLKillers.API.Migrations
{
    /// <inheritdoc />
    public partial class pcnew3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchDuration",
                table: "TeamMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchDuration",
                table: "TeamMatchSummaryStats");
        }
    }
}
