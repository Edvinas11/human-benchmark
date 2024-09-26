using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AimReactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGameAndTargetModelsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameConfigs_GameConfigId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameConfigs");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameConfigId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameConfigId",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "GameType",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameType",
                table: "Games",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "GameConfigId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GameConfigs",
                columns: table => new
                {
                    GameConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameDuration = table.Column<int>(type: "int", nullable: false),
                    GameType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTargets = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetSpeed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameConfigs", x => x.GameConfigId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameConfigId",
                table: "Games",
                column: "GameConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameConfigs_GameConfigId",
                table: "Games",
                column: "GameConfigId",
                principalTable: "GameConfigs",
                principalColumn: "GameConfigId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
