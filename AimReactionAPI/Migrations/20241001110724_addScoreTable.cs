using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class addScoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameType",
                table: "Scores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameType",
                table: "Scores");
        }
    }
}
