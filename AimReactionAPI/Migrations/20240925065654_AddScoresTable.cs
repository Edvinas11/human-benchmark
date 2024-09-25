using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddScoresTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2024, 9, 25, 6, 56, 54, 426, DateTimeKind.Utc).AddTicks(5430));

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2024, 9, 25, 6, 56, 54, 426, DateTimeKind.Utc).AddTicks(5430));

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "ScoreId", "GameType", "Timestamp", "Value" },
                values: new object[,]
                {
                    { 3, 2, new DateTime(2024, 9, 25, 6, 56, 54, 426, DateTimeKind.Utc).AddTicks(5430), 130 },
                    { 4, 0, new DateTime(2024, 9, 25, 6, 56, 54, 426, DateTimeKind.Utc).AddTicks(5430), 175 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2024, 9, 25, 6, 51, 42, 214, DateTimeKind.Utc).AddTicks(3290));

            migrationBuilder.UpdateData(
                table: "Scores",
                keyColumn: "ScoreId",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2024, 9, 25, 6, 51, 42, 214, DateTimeKind.Utc).AddTicks(3300));
        }
    }
}
