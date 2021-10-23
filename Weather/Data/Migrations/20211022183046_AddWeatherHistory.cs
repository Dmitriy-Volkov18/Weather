using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Weather.Data.Migrations
{
    public partial class AddWeatherHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "TemperatureC",
                table: "Weathers");

            migrationBuilder.AlterColumn<string>(
                name: "CityName",
                table: "Weathers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WeatherHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemperatureC = table.Column<int>(type: "int", nullable: false),
                    WeatherEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherHistories_Weathers_WeatherEntityId",
                        column: x => x.WeatherEntityId,
                        principalTable: "Weathers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherHistories_WeatherEntityId",
                table: "WeatherHistories",
                column: "WeatherEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherHistories");

            migrationBuilder.AlterColumn<string>(
                name: "CityName",
                table: "Weathers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Weathers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TemperatureC",
                table: "Weathers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
