using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLKillers.API.Migrations
{
    /// <inheritdoc />
    public partial class summonermatchsummarynewfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoldEarned",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoldSpent",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LongestTimeSpentLiving",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "MagicDamageDealtToChampions",
                table: "SummonerMatchSummaryStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "MatchDuration",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PhysicalDamageDealtToChampions",
                table: "SummonerMatchSummaryStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "RiotTeamId",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Spell1Casts",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Spell2Casts",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Spell3Casts",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Spell4Casts",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SummonerSpell1Casts",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SummonerSpell2Casts",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeSpentDead",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TotalDamageDealtToChampions",
                table: "SummonerMatchSummaryStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "VisionScore",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WardsPlaced",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoldEarned",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "GoldSpent",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "LongestTimeSpentLiving",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "MagicDamageDealtToChampions",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "MatchDuration",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "PhysicalDamageDealtToChampions",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "RiotTeamId",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "Spell1Casts",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "Spell2Casts",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "Spell3Casts",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "Spell4Casts",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "SummonerSpell1Casts",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "SummonerSpell2Casts",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TimeSpentDead",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TotalDamageDealtToChampions",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "VisionScore",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "WardsPlaced",
                table: "SummonerMatchSummaryStats");
        }
    }
}
