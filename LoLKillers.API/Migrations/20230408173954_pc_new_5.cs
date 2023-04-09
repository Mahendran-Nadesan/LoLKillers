using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLKillers.API.Migrations
{
    /// <inheritdoc />
    public partial class pcnew5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SummonerName",
                table: "SummonerMatchSummaryStats",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SummonerName",
                table: "SummonerMatchSummaryStats");
        }
    }
}
