using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLKillers.API.Migrations
{
    /// <inheritdoc />
    public partial class pcnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SummonerSpell1Id",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SummonerSpell2Id",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TeamMatchSummaryStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    RiotMatchId = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    QueueType = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    RiotTeamId = table.Column<int>(type: "int", nullable: false),
                    TeamKills = table.Column<int>(type: "int", nullable: false),
                    TeamDeaths = table.Column<int>(type: "int", nullable: false),
                    TeamAssists = table.Column<int>(type: "int", nullable: false),
                    IsWin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMatchSummaryStats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "SummonerSpell1Id",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "SummonerSpell2Id",
                table: "SummonerMatchSummaryStats");
        }
    }
}
