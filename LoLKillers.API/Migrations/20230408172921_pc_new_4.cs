using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLKillers.API.Migrations
{
    /// <inheritdoc />
    public partial class pcnew4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchDuration",
                table: "SummonerMatchSummaryStats");

            migrationBuilder.AddColumn<bool>(
                name: "TeamFirstBlood",
                table: "TeamMatchSummaryStats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TeamGoldEarned",
                table: "TeamMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamGoldSpent",
                table: "TeamMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TeamMagicDamageDealtToChampions",
                table: "TeamMatchSummaryStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "TeamMinionsKilled",
                table: "TeamMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "TeamPhysicalDamageDealtToChampions",
                table: "TeamMatchSummaryStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TeamTotalDamageDealtToChampions",
                table: "TeamMatchSummaryStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "TeamVisionScore",
                table: "TeamMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamWardsPlaced",
                table: "TeamMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamFirstBlood",
                table: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TeamGoldEarned",
                table: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TeamGoldSpent",
                table: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TeamMagicDamageDealtToChampions",
                table: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TeamMinionsKilled",
                table: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TeamPhysicalDamageDealtToChampions",
                table: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TeamTotalDamageDealtToChampions",
                table: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TeamVisionScore",
                table: "TeamMatchSummaryStats");

            migrationBuilder.DropColumn(
                name: "TeamWardsPlaced",
                table: "TeamMatchSummaryStats");

            migrationBuilder.AddColumn<int>(
                name: "MatchDuration",
                table: "SummonerMatchSummaryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
