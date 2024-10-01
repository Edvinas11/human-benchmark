using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class scoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kazkas",
                table: "Scores",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kazkas",
                table: "Scores",
                nullable: false,
                defaultValue: 0);
        }
    }
}
