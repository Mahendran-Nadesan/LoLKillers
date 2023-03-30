using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLKillers.API.Migrations
{
    /// <inheritdoc />
    public partial class addedsummonerdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SummonerLastUpdatedDate",
                table: "Summoners",
                type: "DATETIMEOFFSET",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SummonerMatchesLastUpdatedDate",
                table: "Summoners",
                type: "DATETIMEOFFSET",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SummonerLastUpdatedDate",
                table: "Summoners");

            migrationBuilder.DropColumn(
                name: "SummonerMatchesLastUpdatedDate",
                table: "Summoners");
        }
    }
}
