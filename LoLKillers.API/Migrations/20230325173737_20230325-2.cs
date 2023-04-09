using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLKillers.API.Migrations
{
    /// <inheritdoc />
    public partial class _202303252 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SummonerId",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "SummonerId",
                table: "SummonerMatchChampStats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SummonerId",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SummonerId",
                table: "SummonerMatchChampStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
