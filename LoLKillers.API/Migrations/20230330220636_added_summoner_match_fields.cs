using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLKillers.API.Migrations
{
    /// <inheritdoc />
    public partial class addedsummonermatchfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstBlood",
                table: "SummonerMatchSummaryStats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FirstBloodAssist",
                table: "SummonerMatchSummaryStats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MinionsKilled",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstBlood",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "FirstBloodAssist",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "MinionsKilled",
                table: "SummonerMatchSummaryStats");
        }
    }
}
